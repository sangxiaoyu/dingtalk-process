using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class MediaUploadResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回描述
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 媒体文件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 媒体文件上传后获取的唯一标识。
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 媒体文件上传时间戳。
        /// </summary>
        public long created_at { get; set; }
    }
}
