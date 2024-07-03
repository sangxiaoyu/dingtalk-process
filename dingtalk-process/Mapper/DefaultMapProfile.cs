using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class DefaultMapProfile : Profile
    {
        /// <summary>
        /// 配置映射
        /// </summary>
        public DefaultMapProfile()
        {
            //映射面对null时，赋值为空字符串
            ValueTransformers.Add<string>(e => e ?? string.Empty);
            #region
            CreateMap<ProcessInstanceDecorateMap,ProcessInstanceRequest>();
            #endregion
        }
    }
}
