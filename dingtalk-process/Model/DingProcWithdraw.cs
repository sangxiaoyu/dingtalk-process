using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DingProcWithdraw
    {
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public string processInstanceId { get; set; }
        /// <summary>
        /// 是否通过系统操作。true：由系统直接终止
        /// false：由指定的操作者终止（需要传发起人才能撤销）
        /// </summary>
        public bool isSystem { get; set; }
        /// <summary>
        /// 终止说明。
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 操作人的userId。
        /// </summary>
        public string operatingUserId { get; set; }
    }
}
