using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class Constant
    {
        /// <summary>
        /// 企业corpid, 需要修改成开发者所在企业
        /// </summary>
        public string CORP_ID { get; set; }
        /// <summary>
        /// 应用的AppKey，登录开发者后台，点击应用管理，进入应用详情可见
        /// </summary>
        public string APPKEY { get; set; }
        /// <summary>
        /// 应用的AppSecret，登录开发者后台，点击应用管理，进入应用详情可见
        /// </summary>
        public string APPSECRET { get; set; }
        /// <summary>
        ///  数据加密密钥。用于回调数据的加密，长度固定为43个字符，从a-z, A-Z, 0-9共62个字符中选取,您可以随机生成
        /// </summary>
        public string ENCODING_AES_KEY { get; set; }
        /// <summary>
        /// 加解密需要用到的token，企业可以随机填写。如 "12345"
        /// </summary>
        public string TOKEN { get; set; }
        /// <summary>
        /// 应用的agentdId，登录开发者后台可查看
        /// </summary>
        public long AGENTID { get; set; }
        /// <summary>
        /// 审批模板唯一标识，可以在审批管理后台找到
        /// </summary>
        public string PROCESS_CODE { get; set; }
        /// <summary>
        /// 回调host
        /// </summary>
        public string CALLBACK_URL_HOST { get; set; }
        /// <summary>
        /// 钉钉基础应用接口
        /// </summary>
        public string DING_URL_API { get; set; }
        /// <summary>
        /// 钉钉OA应用接口
        /// </summary>
        public string DING_URL_OAAPI { get; set; }
    }
}
