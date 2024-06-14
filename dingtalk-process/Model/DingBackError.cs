using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Model
{
    public class DingBackError
    {
        /// <summary>
        /// 请求id
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
    public class DingMsg
    {
        /// <summary>
        /// 请求ID。
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// 创建的异步发送任务ID。
        /// </summary>
        public long task_id { get; set; }
        /// <summary>
        /// 返回码。
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回码描述。
        /// </summary>
        public string errmsg { get; set; }

    }
    public class DingUserUnoinIdMsg<T> : DingMsg
    {
        /// <summary>
        /// 返回数据内容
        /// </summary>
        public T result { get; set; }
    }
    public class DingUserMsg<T> : DingMsg
    {
        /// <summary>
        /// 返回数据内容
        /// </summary>
        public List<T> result { get; set; }
    }
    public class DingUserListMsg<T>
    {
        /// <summary>
        /// 返回码。
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回码描述。
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 请求ID。
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// 返回数据内容
        /// </summary>
        public Result<T> result { get; set; }
    }
    public class Result<T>
    {
        /// <summary>
        /// 是否有更多
        /// </summary>
        public bool has_more { get; set; }
        /// <summary>
        /// 下一次分页的游标。
        /// </summary>
        public int next_cursor { get; set; }
        /// <summary>
        /// 返回用户数据内容
        /// </summary>
        public List<T> list { get; set; }
    }
    public class AuthCode
    {
        /// <summary>
        /// 用户在钉钉上面的昵称
        /// </summary>
        public string nick { get; set; }
        /// <summary>
        /// 用户在当前开放应用所属企业的唯一标识
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 用户在当前开放应用内的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 钉钉id
        /// </summary>
        public string dingId { get; set; }
        /// <summary>
        /// 用户主企业是否达到高级认证级别。
        /// </summary>
        public bool main_org_auth_high_level { get; set; }
    }
    public class UserByUnionId
    {
        /// <summary>
        /// 联系类型： 0：企业内部员工 1：企业外部联系人
        /// </summary>
        public int contact_type { get; set; }
        /// <summary>
        /// 用户的userid
        /// </summary>
        public string userid { get; set; }
    }
}
