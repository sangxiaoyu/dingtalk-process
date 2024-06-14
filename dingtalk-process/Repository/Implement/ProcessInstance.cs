﻿using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace dingtalk_process.Repository
{
    public class ProcessInstance : IProcessInstance
    {
        public readonly AccessTokenUtil _access;
        private readonly Constant _constant;
        public ProcessInstance(AccessTokenUtil access, IOptions<Constant> constant)
        {
            _access = access;
            _constant = constant.Value;
        }
        /// <summary>
        /// 发起审批实例
        /// </summary>
        /// <param name="process">审批实例所需要的body参数</param>
        /// <returns></returns>
        public async Task<DingResponseModel<dynamic>> StartProcessInstance(ProcessInstanceRequest process)
        {
            var response = new DingResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("x-acs-dingtalk-access-token", _access.GetAccessToken(false));
                    var request = await client.PostAsync($"{_constant.DING_URL_API}/workflow/processInstances", JsonContent.Create(process));
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<ProcessInstanceModel>();
                        response.result = response_model ?? new ProcessInstanceModel();
                        return response;
                    }
                    else
                    {
                        response.success = false;
                        response.result = (await request.Content.ReadFromJsonAsync<ErrorBack>()) ?? new ErrorBack();
                        throw new OApiException((int)request.StatusCode, await request.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (OApiException error)
            {
                error.printStackTrace();
            }
            return response;
        }
        /// <summary>
        /// 获取当前企业可看的流程模板
        /// </summary>
        /// <param name="userId">用户钉钉 id</param>
        /// <returns></returns>
        public async Task<DingResponseModel<dynamic>> GetFailEventResult()
        {
            var response = new DingResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token_info = _access.GetAccessToken(true);
                    var request = await client.GetAsync($"{_constant.DING_URL_OAAPI}/call_back/get_call_back_failed_result?access_token={token_info}");
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<DingEventFailResult>();
                        response.result = response_model ?? new DingEventFailResult();
                        return response;
                    }
                    else
                    {
                        response.success = false;
                        var response_model = await request.Content.ReadFromJsonAsync<ErrorBack>();
                        response.result = response_model ?? new ErrorBack();
                        throw new OApiException((int)request.StatusCode, await request.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (OApiException error)
            {
                error.printStackTrace();
            }
            return response;
        }
    }
}
