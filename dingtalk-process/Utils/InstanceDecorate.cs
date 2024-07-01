using dingtalk_process.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public class InstanceDecorate
    {
        private readonly DingSpaceMediaUtil _dingFile;
        public InstanceDecorate(DingSpaceMediaUtil dingFile)
        {
            _dingFile = dingFile;
        }
        public ProcessInstanceRequest GenerateForms(ProcessInstanceDecorateMap decorateMap)
        {
            try
            {
                var requestBody = new ProcessInstanceRequest();
                requestBody.ccList = decorateMap.ccList;
                requestBody.deptId = decorateMap.deptId;
                requestBody.processCode = decorateMap.processCode;
                requestBody.ccPosition = decorateMap.ccPosition;
                requestBody.approvers = decorateMap.approvers;
                requestBody.originatorUserId = decorateMap.originatorUserId;
                requestBody.microappAgentId = decorateMap.microappAgentId;
                List<FormComponentValueVO> result = new List<FormComponentValueVO>();
                if (CollectionUtil.isEmpty(decorateMap.textForms))
                {
                    result.AddRange(decorateMap.textForms.Select(v => new FormComponentValueVO { name = v.name, value = !String.IsNullOrWhiteSpace(v.value) ? v.value : "无" }));
                }
                if (CollectionUtil.isEmpty(decorateMap.attachments) && CollectionUtil.isEmpty(decorateMap.attachments.Select(e => e.attachs).ToList()))
                {
                    foreach (var item in decorateMap.attachments)
                    {
                        if (item.attachs.Count == 0) continue;
                        var fileList = _dingFile.DingFileCommitBatch(decorateMap.originatorUserId, decorateMap.agentId, decorateMap.unionId, item.attachs).Result;
                        var fileResponse = new List<DingFileDentryModel>();
                        foreach (var e in item.attachs)
                        {
                            fileResponse.Add(_dingFile.DingFileCommit(decorateMap.originatorUserId, decorateMap.agentId, decorateMap.unionId, e).Result);
                        }
                        result.Add(new FormComponentValueVO
                        {
                            name = item.name,
                            value = JsonSerializer.Serialize(fileResponse.Select(v => new AttachmentComponentValue
                            {
                                spaceId = v?.dentry.spaceId ?? String.Empty,
                                fileSize = v?.dentry?.size ?? 0,
                                fileId = v?.dentry?.id ?? String.Empty,
                                fileName = v?.dentry?.name ?? String.Empty,
                                fileType = v?.dentry?.type ?? String.Empty
                            }))
                        });
                    }
                }
                //表单明细
                if (CollectionUtil.isEmpty(decorateMap.detailForms))
                {
                    decorateMap.detailForms.ForEach(e => e.textForms.ForEach(e => e.ForEach(e => { if (string.IsNullOrWhiteSpace(e.value)) e.value = "无"; })));
                    if (decorateMap.detailForms.Any(v => v.textForms.Count > 150)) throw new OApiException((int)HttpStatusCode.BadRequest, "单个控件限制数量最大150");
                    var formDetail = decorateMap.detailForms.GroupBy(g => g.name).ToDictionary(e => e.Key, v => v.ToList())
                        .Select(v => new FormComponentValueVO
                        {
                            name = v.Key,
                            value = JsonSerializer.Serialize(v.Value.SelectMany(c => c.textForms))
                        }
                        ).ToList();
                    result.AddRange(formDetail);
                }
                requestBody.formComponentValues = result;
                return requestBody;
            }
            catch (OApiException error)
            {

                throw new OApiException(400, error.Message);
            }
        }
    }
}
