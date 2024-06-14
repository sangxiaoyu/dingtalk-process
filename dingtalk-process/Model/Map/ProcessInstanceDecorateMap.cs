using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class ProcessInstanceDecorateMap
    {
        /// <summary>
        /// 审批发起人
        /// </summary>
        public string originatorUserId { get; set; }
        /// <summary>
        /// 当有附件时，需要传递用户unionId
        /// </summary>
        public string? unionId { get; set; }
        /// <summary>
        /// 审批流的唯一码
        /// </summary>
        public string processCode { get; set; }
        /// <summary>
        /// 审批发起人部门id
        /// </summary>
        public long deptId { get; set; }
        /// <summary>
        /// 代理id
        /// </summary>
        public long agentId { get; set; }
        /// <summary>
        /// 应用标识AgentId
        /// </summary>
        public string? microappAgentId { get; set; }
        /// <summary>
        /// 审批人列表，不使用审批流模板时，直接指定的审批人列表，最大列表长度：20。
        /// </summary>
        public List<Approver>? approvers { get; set; }

        /// <summary>
        /// 抄送人列表
        /// </summary>
        public string? ccList { get; set; }
        /// <summary>
        /// 抄送时机 {START：开始时抄送,FINISH：结束时抄送,START_FINISH：开始和结束时都抄送}
        /// </summary>
        public string? ccPosition { get; set; }
        /// <summary>
        /// 自选节点的规则key。
        /// </summary>
        public List<TargetSelectActioners>? targetSelectActioners { get; set; }
        /// <summary>
        /// 统一单行、多行基础表单控件 例如：{"name":"value"}，{"name":"[\"value1\",\"value2\"]"}，{"[\"开始时间\",\"结束时间\"]","[\"2019-02-19\",\"2019-02-25\"]"}
        /// </summary>
        public List<TextForm>? textForms { get; set; }
        /// <summary>
        /// 明细表单数据
        /// </summary>
        public List<DetailForm> detailForms { get; set; }
        /// <summary>
        /// 统一附件信息
        /// </summary>
        public List<Attachment>? attachments { get; set; }
    }
    /// <summary>
    /// 审批人列表
    /// </summary>
    public class Approver
    {
        /// <summary>
        /// 审批类型，取值：{AND：会签,OR：或签,NONE：单人审批}
        /// </summary>
        public string? actionType { get; set; }
        /// <summary>
        /// 审批人 userId。 例如：["user001","user002"]
        /// </summary>
        public string? userIds { get; set; }
    }
    public class TextForm
    {
        /// <summary>
        /// 控件别名
        /// </summary>
        public string? bizAlias { get; set; }
        /// <summary>
        /// 表单控件名称
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// 表单值
        /// </summary>
        public string? value { get; set; }
    }
    public class PictureForm
    {
        /// <summary>
        /// 表单控件名称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 表单值，数组格式
        /// </summary>
        public List<string>? value { get; set; }
    }
    public class DetailForm
    {
        /// <summary>
        /// 表单控件名称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 明细里的文本控件列表
        /// </summary>
        public List<List<TextForm>>? textForms { get; set; }
    }
    public class Attachment
    {
        /// <summary>
        /// 表单控件名称
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public List<FileBaseInfo> attachs { get; set; }
    }
    public class FileBaseInfo
    {
        /// <summary>
        /// 文件base64
        /// </summary>
        public string file_base64 { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
    }
    /// <summary>
    /// 附件内容
    /// </summary>
    public class AttachmentComponentValue
    {
        /// <summary>
        /// 钉钉存储空间Id
        /// </summary>
        public string spaceId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long fileSize { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string fileType { get; set; }
        /// <summary>
        /// 文件id
        /// </summary>
        public string fileId { get; set; }
    }
    /// <summary>
    /// 自选节点
    /// </summary>
    public class TargetSelectActioners
    {
        /// <summary>
        /// 自选节点的规则key
        /// </summary>
        public string? actionerKey { get; set; }

        /// <summary>
        /// 操作人 userId,例如：["xxxx"]
        /// </summary>
        public string? actionerUserIds { get; set; }
    }
}
