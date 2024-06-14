using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dingtalk_process.Utils;

namespace dingtalk_process
{
    public class OApiException : Exception
    {
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public OApiException(int errCode, string errMsg) : base($"error code: {errCode}, error message:{errMsg}")
        {
            ErrCode = errCode;
            ErrMsg = errMsg;
        }
        public void printStackTrace()
        {
            Log.Error($"errCode={ErrCode}，errMsg={ErrMsg}");
            Console.WriteLine($"errCode={ErrCode} errMsg={ErrMsg}");
        }
    }
}
