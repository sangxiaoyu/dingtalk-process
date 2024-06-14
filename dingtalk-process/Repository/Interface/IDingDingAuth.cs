using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Repository
{
    public interface IDingDingAuth
    {
        /// <summary>
        /// 获取企业内部应用token，原始过期时间为7200s
        /// 此处以7000s为准，如过期则重新获取
        /// </summary>
        /// <param name="refresh">true:从钉钉获取，false:缓存获取</param>
        /// <returns></returns>
        Task<string> GetToken(bool refresh = false);
        /// <summary>
        /// 获取相应应用的key和scecret创建密钥
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appsecret"></param>
        /// <param name="isRefresh"></param>
        /// <returns></returns>
        Task<string> GetToken(string appkey, string appsecret, bool isRefresh = false);
    }
}
