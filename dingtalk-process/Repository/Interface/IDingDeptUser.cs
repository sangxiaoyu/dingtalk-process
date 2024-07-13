using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public interface IDingDeptUser
    {
        /// <summary>
        /// 获取部门下的一级所有部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingDept(long deptId);
        /// <summary>
        /// 获取部门下的所有成员
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingDeptUsers(long deptId, int cursor, int size);
        /// <summary>
        /// 根据unionid获取用户userid
        /// </summary>
        /// <param name="unionid">员工在当前开发者企业账号范围内的唯一标识，系统生成，不会改变。</param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingUserFromUnionId(string unionid);
        /// <summary>
        /// 根据userid获取用户信息
        /// </summary>
        /// <param name="userid">用户的userId</param>
        /// <returns></returns>
        Task<ResponseModel<dynamic>> DingUserFromUserId(string userid);
    }
}
