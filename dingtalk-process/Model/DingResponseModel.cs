using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    /// <summary>
    /// 响应格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DingResponseModel<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// 响应信息
        /// </summary>
        public T result { get; set; }
    }
    public class DingAutoCdeResponseModel<T>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public T user_info { get; set; }

    }
    public class DingSpaceBack
    {
        /// <summary>
        /// 钉盘空间Id
        /// </summary>
        public long spaceId { get; set; }
    }
    /// <summary>
    /// 统一错误
    /// </summary>
    public class ErrorBack
    {
        /// <summary>
        /// 请求Id
        /// </summary>
        public string requestid { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }
    }
    public class ProcessInstanceModel
    {
        /// <summary>
        /// 审批实例
        /// </summary>
        public string instanceId { get; set; }
    }
}
