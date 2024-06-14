using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Repository
{
    public interface IProcessInstance
    {
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<DingResponseModel<dynamic>> StartProcessInstance(ProcessInstanceRequest process);
        /// <summary>
        /// 获取当前企业所有可管理的表单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DingResponseModel<dynamic>> GetFailEventResult();
    }
}
