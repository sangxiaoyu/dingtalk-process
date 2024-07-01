using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Model
{
    public class DingFileModel
    {
        /// <summary>
        /// 上传唯一标识。
        /// </summary>
        public string uploadKey { get; set; }
        /// <summary>
        /// 文件存储类型。
        /// </summary>
        public string storageDriver { get; set; }
        /// <summary>
        /// 上传协议。
        /// </summary>
        public string protocol { get; set; }
        /// <summary>
        /// Header加签上传信息。
        /// </summary>
        public HeaderSignature headerSignatureInfo { get; set; }
    }
    public class HeaderSignature
    {
        /// <summary>
        /// 多个上传下载URL列表。
        /// </summary>
        public List<string> resourceUrls { get; set; }
        /// <summary>
        /// 请求头信息。
        /// </summary>
        public Dictionary<string, string> headers { get; set; }
        /// <summary>
        /// 请求头信息。
        /// </summary>
        public int expirationSeconds { get; set; }
        /// <summary>
        /// 地域。
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// 内网访问地址。
        /// </summary>
        public List<string> internalResourceUrls { get; set; }
    }
}
