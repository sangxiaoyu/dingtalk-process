using dingtalk_process.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class ProcessInstanceRequest
    {
        /// <summary>
        /// 审批发起人
        /// </summary>
        public string originatorUserId { get; set; }
        /// <summary>
        /// 审批流的唯一码
        /// </summary>
        public string processCode { get; set; }
        /// <summary>
        /// 审批发起人部门id
        /// </summary>
        public long deptId { get; set; }
        /// <summary>
        /// 应用标识AgentId
        /// </summary>
        public string? microappAgentId { get; set; }
        /// <summary>
        /// 审批人列表，不使用审批流模板时，直接指定的审批人列表，最大列表长度：20。
        /// </summary>
        public List<Approver>? approvers { get; set; }
        /// <summary>
        /// 抄送人列表
        /// </summary>
        public string? ccList { get; set; }
        /// <summary>
        /// 抄送时机 {START：开始时抄送,FINISH：结束时抄送,START_FINISH：开始和结束时都抄送}
        /// </summary>
        public string? ccPosition { get; set; }
        /// <summary>
        /// 表单数据内容，控件列表，最大列表长度：150。
        /// </summary>
        public List<FormComponentValueVO> formComponentValues { get; set; }

    }
}
