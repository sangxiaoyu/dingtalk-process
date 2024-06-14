using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class FormComponentValueVO
    {
        /// <summary>
        /// 扩展值
        /// </summary>
        public string? extValue { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 控件值
        /// </summary>
        public string value { get; set; } = String.Empty;
    }
}
