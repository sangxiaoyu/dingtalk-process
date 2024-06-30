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
        Task<ResponseModel<dynamic>> StartProcessInstance(ProcessInstanceRequest process);
        /// <summary>
        /// 获取当前企业所有可管理的表单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> GetFailEventResult();
        /// <summary>
        /// 撤销审批实例
        /// 备注：审批发起15秒内不能撤销审批流程。
        /// 当入参isSystem选择为false时（由指定的操作者终止），需要传发起人才能撤销。
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<string> WithdrawProcessInstance(DingProcessWithdraw process);
        /// <summary>
        /// 单个审批流程实例详情
        /// </summary>
        /// <param name="processInstanceId">审批实例详情id</param>
        /// <returns></returns>
        Task<string> SingleProcessInstance(string processInstanceId);
    }
}
