using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DingFileConfigModel
    {
        /// <summary>
        /// 添加文件唯一标识。
        /// </summary>
        public string uploadKey { get; set; }
        /// <summary>
        /// 文件的名称，带后缀。
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父目录Id。根目录时，该参数是0。
        /// </summary>
        public string parentId { get; set; } = "0";
        /// <summary>
        ///可选参数
        /// </summary>
        public CommitOption option { get { return new CommitOption(); } set { } }
    }
    public class CommitOption
    {
        /// <summary>
        /// 文件大小，单位Byte。
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 文件名称冲突策略。
        /// </summary>
        public string conflictStrategy { get; set; } = "AUTO_RENAME";
    }
}
