using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DingFileDentryModel
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        public Dentry dentry { get; set; }
    }
    public class Dentry
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 所在空间Id
        /// </summary>
        public string spaceId { get; set; }
        /// <summary>
        /// parentId
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 类型，本接口返回类型只有FILE。
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文件大小，单位Byte。
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 文件在空间内的路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public long version { get; set; }
        /// <summary>
        /// 状态。 NORMAL：正常 DELETED：已删除 EXPIRED：已过期
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string extension { get; set; }
        /// <summary>
        /// 创建者unionId
        /// </summary>
        public string creatorId { get; set; }
        /// <summary>
        /// 修改者unionId
        /// </summary>
        public string modifierId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 修改时间，
        /// </summary>
        public string modifiedTime { get; set; }
    }
}
