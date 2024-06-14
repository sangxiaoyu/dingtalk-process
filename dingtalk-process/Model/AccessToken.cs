using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Model
{
    public class AccessToken
    {
        /// <summary>
        /// 生成的accessToken。
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// accessToken的过期时间，单位秒
        /// </summary>
        public long expireIn { get; set; }
    }
}
