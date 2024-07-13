using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Model
{
    public class DeptUserModel
    {
        /// <summary>
        /// 用户的userId
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 用户在当前开发者企业帐号范围内的唯一标识
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 国际电话区号。
        /// </summary>
        public string state_code { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 是否号码隐藏：true：隐藏 false：不隐藏
        /// </summary>
        public bool hide_mobile { get; set; }
        /// <summary>
        /// 分机号
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 员工工号
        /// </summary>
        public string job_number { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 员工邮箱。
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 员工的企业邮箱。
        /// </summary>
        public string org_email { get; set; }
        /// <summary>
        /// 办公地点。
        /// </summary>
        public string work_place { get; set; }
        /// <summary>
        ///备注。
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 所属部门id列表。
        /// </summary>
        public List<int> dept_id_list { get; set; }
        /// <summary>
        ///员工在部门中的排序。。
        /// </summary>
        public long dept_order { get; set; }
        /// <summary>
        /// 扩展属性。
        /// </summary>
        public string extension { get; set; }
        /// <summary>
        /// 入职时间，Unix时间戳，单位毫秒。
        /// </summary>
        public long hired_date { get; set; }

        /// <summary>
        /// 是否激活了钉钉： true：已激活 false：未激活
        /// </summary>
        public bool active { get; set; }
        /// <summary>
        /// 是否为企业的管理员： true：是 false：不是
        /// </summary>
        public bool admin { get; set; }
        /// <summary>
        /// 是否为企业的老板： true：是 false：不是
        /// </summary>
        public bool boss { get; set; }
        /// <summary>
        /// 是否是部门的主管： true：是 false：不是
        /// </summary>
        public bool leader { get; set; }
        /// <summary>
        /// 是否专属帐号： true：是 false：不是
        /// </summary>
        public bool exclusive_account { get; set; }
        /// <summary>
        /// 是否离职：{true：是 false ：否}
        /// </summary>
        public bool have_left { get; set; }
    }
}
