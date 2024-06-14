using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DingEventChange
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        public string? EventType { get; set; }
        /// <summary>
        /// 审批模板的唯一码。
        /// </summary>
        public string? processCode { get; set; }
        /// <summary>
        /// 审批实例id。
        /// </summary>
        public string? processInstanceId { get; set; }
        /// <summary>
        /// 审批实例所在的企业corpId。
        /// </summary>
        public string? corpId { get; set; }
        /// <summary>
        /// 结束审批实例时间。时间戳，单位毫秒。
        /// </summary>
        public long? finishTime { get; set; }
        /// <summary>
        /// createTime
        /// </summary>
        public long? createTime { get; set; }
        /// <summary>
        /// 审批实例标题。
        /// </summary>
        public string? title { get; set; }
        /// <summary>
        /// 类型，type为start表示审批实例开始。
        /// type为start表示审批实例开始。
        /// finish：审批正常结束（同意或拒绝）
        /// terminate：审批终止（发起人撤销审批单）
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 发起审批实例的员工userId。
        /// </summary>
        public string? staffId { get; set; }
        /// <summary>
        /// 审批实例url，可在钉钉内跳转到审批页面。
        /// </summary>
        public string? url { get; set; }
        /// <summary>
        /// 正常结束时result为agree，拒绝时result为refuse，审批终止时没这个值。
        /// </summary>
        public string? result { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string? content { get; set; }
        /// <summary>
        /// 表示操作时写的评论内容。
        /// </summary>
        public string? remark { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public long? taskId { get; set; }
    }
}
