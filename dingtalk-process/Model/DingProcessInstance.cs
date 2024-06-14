using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DingProcessInstance
    {
        /// <summary>
        /// 模板图标名。
        /// </summary>
        public string iconName { get; set; }
        /// <summary>
        /// 模版名称
        /// </summary>
        public string flowTitle { get; set; }
        /// <summary>
        /// 模版code
        /// </summary>
        public string processCode { get; set; }
        /// <summary>
        /// 是否为新模版。 true：是 false：不是
        /// </summary>
        public bool newProcess { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime gmtModified { get; set; }
        /// <summary>
        /// 关联考勤类型，取值。 0：无 1：补卡申请 2：请假
        /// </summary>
        public int attendanceType { get; set; }
        /// <summary>
        /// 图标URL地址
        /// </summary>
        public string iconUrl { get; set; }
    }
}
