using dingtalk_process.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Repository
{
    public class DingDeptUser: IDingDeptUser
    {
        public readonly IDingDingAuth _access;
        private readonly Constant _constant;
        public DingDeptUser(IDingDingAuth access, IOptions<Constant> constant)
        {
            _access = access;
            _constant = constant.Value;
        }
        public async Task<ResponseModel<dynamic>> DingDept(long deptId)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(false);
                    var request = await client.PostAsync($"{_constant.DING_URL_OAAPI}/topapi/v2/department/listsub?access_token={token}", JsonContent.Create(new
                    {
                        dept_id = deptId,
                        language = "zh_CN"
                    }));
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<DingUserMsg<DeptModel>>();
                        response.result = response_model?.result ?? new List<DeptModel>();
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

        public async Task<ResponseModel<dynamic>> DingDeptUsers(long deptId, int cursor = 0, int size = 100)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(false);
                    var request = await client.PostAsync($"{_constant.DING_URL_OAAPI}/topapi/v2/user/list?access_token={token}", JsonContent.Create(new
                    {
                        dept_id = deptId,
                        cursor = cursor,
                        size = size,
                    }));
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<DingUserListMsg<DeptUserModel>>();
                        response.result = response_model ?? new DingUserListMsg<DeptUserModel>();
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
        public async Task<ResponseModel<dynamic>> DingUserFromUnionId(string unionid)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(_constant.APPKEY, _constant.APPSECRET);
                    var request = await client.PostAsync($@"{_constant.DING_URL_OAAPI}/topapi/user/getbyunionid?access_token={token}",
                    JsonContent.Create(new
                    {
                        unionid = unionid
                    }));
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<ResponseModel<UserByUnionId>>();
                        response.result = response_model?.result ?? new UserByUnionId();
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

        public async Task<ResponseModel<dynamic>> DingUserFromUserId(string userid)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await _access.GetToken(_constant.APPKEY, _constant.APPSECRET);
                    var request = await client.PostAsync($@"{_constant.DING_URL_OAAPI}/topapi/v2/user/get?access_token={token}",
                    JsonContent.Create(new
                    {
                        language = "zh_CN",
                        userid = userid
                    }));
                    if (request.IsSuccessStatusCode)
                    {
                        var response_model = await request.Content.ReadFromJsonAsync<ResponseModel<DeptUserModel>>();
                        response.result = response_model?.result ?? new DeptUserModel();
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
    }
}
