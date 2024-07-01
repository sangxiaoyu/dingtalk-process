using dingtalk_process.Model;
using dingtalk_process.Repository;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;

namespace dingtalk_process
{
    public class DingSpaceMedia : IDingSpaceMedia
    {
        public readonly IDingDingAuth _access;
        private readonly Constant _constant;
        public DingSpaceMedia(IDingDingAuth access, IOptions<Constant> constant)
        {
            _access = access;
            _constant = constant.Value;
        }
        /// <summary>
        /// 获取钉盘空间id
        /// 每个用户权限不一样，根据用户进行缓存存储spaceId
        /// </summary>
        /// <param name="space">用户信息</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseModel<dynamic>> DingSpace(DingSpaceModel space)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                ObjectCache _cache = MemoryCache.Default;
                //缓存存在则直接读取缓存
                if (_cache.Contains($"{space.userId}_dingspance") && !space.isRefresh)
                {
                    response.result = new DingSpaceBack
                    {
                        spaceId = long.Parse(_cache.Get($"{space.userId}_dingspance").ToString() ?? "0")
                    };
                    return response;
                }
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(false);
                    client.DefaultRequestHeaders.Add("x-acs-dingtalk-access-token", token);
                    var request = await client.PostAsync($"{_constant.DING_URL_API}/workflow/processInstances/spaces/infos/query", JsonContent.Create(space));
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<ResponseModel<DingSpaceBack>>();
                        //获取之后存储到缓存
                        _cache.Set($"{space.userId}_dingspance", response_model?.result.spaceId.ToString() ?? String.Empty,
                            new CacheItemPolicy
                            {
                                SlidingExpiration = TimeSpan.FromSeconds(60) //滑动刷新，如果1分钟内有获取过则重新刷新到设置时间
                            });
                        response.result = response_model?.result ?? new DingSpaceBack();
                        return response;
                    }
                    else
                    {
                        response.success = false;
                        response.result = (await request.Content.ReadFromJsonAsync<ErrorBack>()) ?? new ErrorBack();
                        throw new OApiException((int)request.StatusCode, await request.Content.ReadAsStringAsync());
                    }
                }

            }
            catch (OApiException error)
            {
                error.printStackTrace();
            }
            return response;
        }
        /// <summary>
        /// 钉盘文件上传信息
        /// </summary>
        /// <param name="spaceId">钉盘空间Id</param>
        /// <param name="unionId">用户联合Id</param>
        /// <param name="space">获取文件上传信息所需要的实体</param>
        /// <returns></returns>
        public async Task<ResponseModel<dynamic>> DingFileInfo(long spaceId, string unionId, DingFileInfo space)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(false);
                    client.DefaultRequestHeaders.Add("x-acs-dingtalk-access-token", token);
                    var request = await client.PostAsync($"{_constant.DING_URL_API}/storage/spaces/{spaceId}/files/uploadInfos/query?unionId={unionId}",
                        JsonContent.Create(space));

                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<DingFileModel>();
                        response.result = response_model ?? new DingFileModel();
                        return response;
                    }
                    else
                    {
                        response.success = false;
                        response.result = (await request.Content.ReadFromJsonAsync<ErrorBack>()) ?? new ErrorBack();
                        return response;
                    }
                }

            }
            catch (OApiException error)
            {
                error.printStackTrace();
            }
            return response;
        }
        #region 重写方法多方式实现文件上传
        /// <summary>
        /// 2.第二步 使用OSS的header加签上传文件
        /// </summary>
        /// <param name="fileBase">base64</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="dingFile"></param>
        /// <returns></returns>
        public async Task<bool> DingFilesOSS(byte[] fileBase, DingFileModel dingFile)
        {
            using (var client = new HttpClient())
            {
                //此处使用上传文件信息接口返回的OSS地址和加签信息
                foreach (var item in dingFile?.headerSignatureInfo?.headers ?? new Dictionary<string, string>())
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                var response = await client.PutAsync(dingFile?.headerSignatureInfo?.resourceUrls.First(), new ByteArrayContent(fileBase));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        ///  2.第二步 使用OSS的header加签上传文件
        /// </summary>
        /// <param name="fileInfo">文件信息。为文件路径/base64</param>
        /// <param name="dingFile"></param>
        /// <returns></returns>
        public async Task<bool> DingFilesOSS(string fileInfo, DingFileModel dingFile)
        {
            using (var client = new HttpClient())
            {
                foreach (var item in dingFile?.headerSignatureInfo?.headers ?? new Dictionary<string, string>())
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                var response = new HttpResponseMessage();
                if (JudgeStringUtil.IsBase64(fileInfo, out byte[] bytes))
                {
                    response = await client.PutAsync(dingFile?.headerSignatureInfo.resourceUrls.First(), new ByteArrayContent(bytes));
                }
                else
                {
                    // 读取文件
                    Stream fileStream = new FileStream(fileInfo, FileMode.Open, FileAccess.Read);
                    response = await client.PutAsync(dingFile?.headerSignatureInfo.resourceUrls.First(), new StreamContent(fileStream));
                }
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
        /// <summary>
        /// 文件提交
        /// </summary>
        /// <param name="spaceId">钉盘空间</param>
        /// <param name="unionId">用户unionId</param>
        /// <param name="commit">提交body</param>
        /// <returns></returns>
        public async Task<ResponseModel<dynamic>> DingFilesCommit(long spaceId, string unionId, DingFileConfigModel commit)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(false);
                    client.DefaultRequestHeaders.Add("x-acs-dingtalk-access-token", token);
                    var request = await client.PostAsync($"{_constant.DING_URL_API}/storage/spaces/{spaceId}/files/commit?unionId={unionId}",
                        JsonContent.Create(commit));

                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<DingFileDentryModel>();
                        response.result = response_model ?? new DingFileDentryModel();
                        return response;
                    }
                    else
                    {
                        response.success = false;
                        response.result = (await request.Content.ReadFromJsonAsync<ErrorBack>()) ?? new ErrorBack();
                        throw new OApiException((int)request.StatusCode, await request.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (OApiException error)
            {
                error.printStackTrace();
            }
            return response;
        }
        /// <summary>
        /// 钉盘文件授权
        /// </summary>
        /// <param name="spaceId">钉盘空间id</param>
        /// <param name="unionId">当前用户unionId</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseModel<dynamic>> DingFilePermissions(long spaceId, string unionId, DingFilePermission filePermission)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(false);
                    client.DefaultRequestHeaders.Add("x-acs-dingtalk-access-token", token);
                    var request = await client.PostAsync($"{_constant.DING_URL_API}/storage/spaces/{spaceId}/dentries/0/permissions?unionId={unionId}",
                        JsonContent.Create(filePermission));

                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<ResponseModel<string>>();
                        response.success = response_model?.success ?? true;
                        return response;
                    }
                    else
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<ErrorBack>();
                        response.success = false;
                        response.result = response_model ?? new ErrorBack();
                        throw new OApiException((int)request.StatusCode, await request.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (OApiException error)
            {
                error.printStackTrace();
            }
            return response;
        }
        #region 多媒体文件上传
        /// <summary>
        /// 此处后期需要优化
        /// </summary>
        /// <param name="type">文件类型</param>
        /// <param name="file">文件物理地址</param>
        /// <returns></returns>
        public async Task<MediaUploadResult> upload(string type, string file)
        {
            var result = string.Empty;
            var token = await _access.GetToken(false);
            var request = (HttpWebRequest)WebRequest.Create($"{_constant.DING_URL_OAAPI}/media/upload?access_token={token}&type={type}");
            var boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "–-\r\n");
                var filename = Path.GetFileName(file);
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    byte[] bArr = new byte[fs.Length];
                    fs.Read(bArr, 0, bArr.Length);
                    requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                    var header = $"Content-Disposition:form-data;name=\"media\";filename=\"{filename}\"\r\nfilelength=\"{fs.Length}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
                    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(header.ToString());
                    requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    fs.Close();
                    requestStream.Write(bArr, 0, bArr.Length);
                    requestStream.Write(trailer, 0, trailer.Length);
                }
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            var resultModel = JsonSerializer.Deserialize<MediaUploadResult>(result);
            return resultModel ?? new MediaUploadResult();
        }
        /// <summary>
        /// 此处待优化
        /// </summary>
        /// <param name="type">文件类型</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">文件字节流</param>
        /// <returns></returns>
        public async Task<MediaUploadResult> upload(string type, string filename, byte[] file)
        {
            var result = string.Empty;
            var token = await _access.GetToken(false);
            var request = (HttpWebRequest)WebRequest.Create($"{_constant.DING_URL_OAAPI}/media/upload?access_token={token}&type={type}");
            var boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "–-\r\n");
                requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                var header = $"Content-Disposition:form-data;name=\"media\";filename=\"{filename}\"\r\nfilelength=\"{file.Length}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(header.ToString());
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                requestStream.Write(file, 0, file.Length);
                requestStream.Write(trailer, 0, trailer.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            var resultModel = JsonSerializer.Deserialize<MediaUploadResult>(result);
            return resultModel ?? new MediaUploadResult();
        }

        #endregion
    }
}
