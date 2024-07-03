using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Model
{
    /// <summary>
    /// 钉盘空间
    /// </summary>
    public class DingSpaceModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 应用的agentId
        /// </summary>
        public long agentId { get; set; }
        /// <summary>
        /// 是否重新获取，不取缓存
        /// </summary>
        public bool isRefresh { get; set; }
    }
    /// <summary>
    /// 钉文件信息
    /// </summary>
    public class DingFileInfo
    {
        /// <summary>
        /// 通过指定上传协议返回不同协议上传所需要的信息。HEADER_SIGNATURE：Header加签
        /// </summary>
        public string protocol { get; set; } = "HEADER_SIGNATURE";
        /// <summary>
        /// 是否需要分片上传。
        /// true：需要 文件大小5G以上
        /// false：不需要 文件大小5G以内
        /// </summary>
        public bool multipart { get; set; } = false;

    }
    /// <summary>
    /// 钉钉文件授权
    /// </summary>
    public class DingFilePermission
    {
        /// <summary>
        ///权限角色Id。OWNER：拥有者 MANAGER：管理者 EDITOR：编辑者 DOWNLOADER：下载者 READER：查看者
        /// </summary>
        public string roleId { get; set; }
        /// <summary>
        /// 是否需要分片上传。
        /// true：需要 文件大小5G以上
        /// false：不需要 文件大小5G以内
        /// </summary>
        public List<Members> members { get; set; }
        /// <summary>
        /// 可选参数。
        /// </summary>
        public Options option { get { return new Options(); } set { } }
    }
    public class Members
    {
        /// <summary>
        /// 权限成员类型：ORG：企业 DEPT：部门 TAG：自定义tag CONVERSATION：会话 USER：用户
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///权限归属的企业corpId。 
        ///type为DEPT或TAG时，该参数必填。
        ///type为USER时，该参数选填。
        /// </summary>
        public string corpId { get; set; } = "";
    }
    public class Options
    {
        /// <summary>
        /// 授权有效时长，单位秒，仅支持APP空间类型。
        /// </summary>
        public long duration { get; set; } = 3600;
    }
}
