using dingtalk_process.Model;

namespace dingtalk_process
{
    /// <summary>
    /// 获取钉盘/文件信息
    /// </summary>
    public interface IDingSpaceMedia
    {
        /// <summary>
        /// 获取钉盘空间id
        /// 备注：一个企业内审批附件钉盘space_id是唯一的。
        /// </summary>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingSpace(DingSpaceModel space);
        /// <summary>
        /// 1.第一步  钉盘文件上传信息
        /// </summary>
        /// <param name="spaceId">钉盘空间Id</param>
        /// <param name="unionId">用户联合Id</param>
        /// <param name="space">获取文件上传信息所需要的实体</param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingFileInfo(long spaceId, string unionId, DingFileInfo space);
        /// <summary>
        /// 钉盘文件授权
        /// </summary>
        /// <param name="spaceId">钉盘空间Id</param>
        /// <param name="unionId">当前用户Id</param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingFilePermissions(long spaceId, string unionId, DingFilePermission filePermission);

        #region 重写方法多方式接收文件
        /// <summary>
        /// 2.第二步 使用OSS的header加签上传文件
        /// </summary>
        /// <param name="fileBase">base64</param>
        /// <param name="dingFile"></param>
        /// <returns></returns>
        Task<bool> DingFilesOSS(byte[] fileBase, DingFileModel dingFile);
        /// <summary>
        ///  2.第二步 使用OSS的header加签上传文件
        /// </summary>
        /// <param name="fileInfo">文件base64/本地文件地址</param>
        /// <param name="dingFile">上传文件信息</param>
        /// <returns></returns>
        Task<bool> DingFilesOSS(string fileInfo, DingFileModel dingFile);
        #endregion

        /// <summary>
        /// 3.第三步 文件提交
        /// </summary>
        /// <param name="spaceId">钉盘空间Id</param>
        /// <param name="unionId">当前用户Id</param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingFilesCommit(long spaceId, string unionId, DingFileConfigModel commit);

        #region 媒体文件上传
        /// <summary>
        /// 媒体文件上传
        /// </summary>
        /// <param name="type">媒体文件类型</param>
        /// <param name="file">文件物理地址</param>
        /// <returns></returns>
        Task<MediaUploadResult> upload(string type, string file);
        /// <summary>
        /// 媒体文件上传
        /// </summary>
        /// <param name="type">媒体文件类型</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">文件字节</param>
        /// <returns></returns>
        Task<MediaUploadResult> upload(string type, string filename, byte[] file);
        #endregion
    }
}
