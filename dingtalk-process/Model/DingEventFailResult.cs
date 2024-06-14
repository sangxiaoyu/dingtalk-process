using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DingEventFailResult
    {
        public int errcode { get; set; }
        public bool hasMore { get; set; }
        public string errmsg { get; set; }
        public List<failed_list> failed_list { get; set; }
    }
    public class failed_list
    {
        public string call_back_tag { get; set; }
        public long event_time { get; set; }
        public bpms_task_change? bpms_task_change { get; set; }
    }
    public class bpms_task_change
    {
        public string corpid { get; set; }
        public DingEventChange? bpmsCallBackData { get; set; }
    }
}
