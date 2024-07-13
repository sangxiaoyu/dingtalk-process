using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Model
{
    public class DeptModel
    {
        /// <summary>
        /// 当前部门Id
        /// </summary>
        public long dept_id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父部门ID
        /// </summary>
        public long parent_id { get; set; }
        /// <summary>
        /// 是否同步创建一个关联此部门的企业群： true：创建 false：不创建
        /// </summary>
        public bool create_dept_group { get; set; }
        /// <summary>
        /// 部门群已经创建后，有新人加入部门是否会自动加入该群： true：会自动入群 false：不会
        /// </summary>
        public bool auto_add_user { get; set; }
    }
}
