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
    }
}
