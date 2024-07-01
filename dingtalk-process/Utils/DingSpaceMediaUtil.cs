using dingtalk_process.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Utils
{
    public class DingSpaceMediaUtil
    {
        /// <summary>
        /// 将钉钉上传文件多个步骤组合成一个步骤
        /// </summary>
        private readonly IDingSpaceMedia _media;
        public DingSpaceMediaUtil(IDingSpaceMedia media)
        {
            _media = media;
        }
        /// <summary>
        /// 上传的附件需要上传到dingding服务器
        /// 1.获取钉钉存储空间
        /// 2.获取文件上传信息
        /// 3.文件钉钉 url OSS 提交
        /// 4.提交文件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="unionId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<DingFileDentryModel> DingFileCommit(string userId, long agentId, string unionId, FileBaseInfo file)
        {

            //1.获取钉钉存储空间
            var spaceInfoModel = new DingSpaceModel()
            {
                agentId = agentId,
                userId = userId,
                isRefresh = false
            };
            try
            {


                var dingSpace = await _media.DingSpace(spaceInfoModel);
                var dingSpaceId = new DingSpaceBack();
                //2.获取上传文件信息
                var dingFileModel = new DingFileInfo();
                if (dingSpace.success)
                {
                    dingSpaceId = dingSpace.result as DingSpaceBack;
                }
                var dingFile = await _media.DingFileInfo(dingSpaceId?.spaceId ?? 0, unionId, dingFileModel);
                var handler_oss = new DingFileModel();
                if (dingFile.success)
                {
                    handler_oss = dingFile.result as DingFileModel;
                }
                else
                {
                    throw new OApiException(400, System.Text.Json.JsonSerializer.Serialize(dingFile));
                }
                //3.文件钉钉 url OSS 提交
                var result = await _media.DingFilesOSS(file.file_base64, handler_oss);
                //4.文件提交
                if (result)
                {
                    var fileCommitInfo = new DingFileConfigModel()
                    {
                        name = file.name,
                        parentId = "0",
                        uploadKey = handler_oss.uploadKey
                    };
                    var fileInfo = await _media.DingFilesCommit(dingSpaceId?.spaceId ?? 0, unionId, fileCommitInfo);
                    return fileInfo.result;
                }
                return new DingFileDentryModel();
            }
            catch (OApiException error)
            {

                throw new OApiException(400, error.Message);
            }
        }
        /// <summary>
        /// 批量上传的附件需要上传到dingding服务器
        /// 1.获取钉钉存储空间
        /// 2.获取文件上传信息
        /// 3.文件钉钉 url OSS 提交
        /// 4.提交文件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="unionId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<List<DingFileDentryModel>> DingFileCommitBatch(string userId, long agentId, string unionId, List<FileBaseInfo> file)
        {
            try
            {


                //1.获取钉钉存储空间
                var spaceInfoModel = new DingSpaceModel()
                {
                    agentId = agentId,
                    userId = userId,
                    isRefresh = false
                };
                var dingSpace = await _media.DingSpace(spaceInfoModel);
                var dingSpaceId = new DingSpaceBack();
                if (dingSpace.success)
                {
                    dingSpaceId = dingSpace.result as DingSpaceBack;
                }
                //2.获取上传文件信息
                var dingFileModel = new DingFileInfo();
                var dingFile = await _media.DingFileInfo(dingSpaceId?.spaceId ?? 0, unionId, dingFileModel);
                if (!dingFile.success)
                {
                    throw new Exception(System.Text.Json.JsonSerializer.Serialize(dingFile.result));
                }
                var commitList = new List<DingFileDentryModel>();
                //3.文件钉钉 url OSS 提交
                foreach (var item in file)
                {
                    if (string.IsNullOrEmpty(item.file_base64)) continue;
                    var result = await _media.DingFilesOSS(item.file_base64, dingFile.result);
                    //4.文件提交
                    if (result)
                    {
                        var fileCommitInfo = new DingFileConfigModel()
                        {
                            name = item.name,
                            parentId = "0",
                            uploadKey = dingFile.result.uploadKey
                        };
                        var fileInfo = await _media.DingFilesCommit(dingSpaceId?.spaceId ?? 0, unionId, fileCommitInfo);
                        commitList.Add(fileInfo.result);
                    }
                }
                return commitList;
            }
            catch (OApiException error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
