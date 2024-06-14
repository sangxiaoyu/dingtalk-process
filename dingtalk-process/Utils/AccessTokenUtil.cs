using dingtalk_process.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class AccessTokenUtil
    {
        private readonly IDingDingAuth _auth;
        public AccessTokenUtil(IDingDingAuth auth)
        {
            _auth = auth;
        }
        /// <summary>
        /// 获取token，异步
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            return await _auth.GetToken();
        }
        /// <summary>
        /// 获取token，异步
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync(string appkey, string appsecret, bool isRefresh = false)
        {
            return await _auth.GetToken(appkey, appsecret, isRefresh);
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="isRefresh"></param>
        /// <returns></returns>
        public string GetAccessToken(bool isRefresh = false)
        {
            return _auth.GetToken(isRefresh).Result;
        }
    }
}
