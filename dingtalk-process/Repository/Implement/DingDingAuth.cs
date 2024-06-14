using dingtalk_process.Model;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime.Caching;

namespace dingtalk_process.Repository
{
    public class DingDingAuth : IDingDingAuth
    {
        private readonly Constant _constant;
        public DingDingAuth(IOptions<Constant> constant)
        {
            _constant = constant.Value;
        }

        /// <summary>
        /// 获取企业内部应用token，原始过期时间为7200s
        /// 此处以7000s为准，如过期则重新获取
        /// </summary>
        /// <param name="isRefresh">true:钉钉获取，false:从缓存获取</param>
        /// <returns></returns>
        public async Task<string> GetToken(bool refresh = false)
        {
            try
            {
                ObjectCache _cache = MemoryCache.Default;
                if (_cache.Contains(_constant.APPKEY) && !refresh)
                {
                    return _cache.Get(_constant.APPKEY) as string;
                }
                var client = new HttpClient();
                var request = await client.PostAsync($"{_constant.DING_URL_API}/oauth2/accessToken", JsonContent.Create(new
                {
                    appKey = _constant.APPKEY,
                    appSecret = _constant.APPSECRET,
                }));
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadFromJsonAsync<AccessToken>();
                    _cache.Set(_constant.APPKEY, response?.accessToken ?? string.Empty, new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds((response?.expireIn ?? 200) - 200),
                    });
                    return response?.accessToken ?? string.Empty;
                }
                else
                {
                    var error = await request.Content.ReadFromJsonAsync<DingBackError>();
                    return error?.message ?? string.Empty;
                }
            }
            catch (OApiException error)
            {
                error?.printStackTrace();
                return error?.Message ?? string.Empty;
            }
        }
        /// <summary>
        /// 获取企业内部应用token，原始过期时间为7200s
        /// 此处以7000s为准，如过期则重新获取
        /// </summary>
        /// <param name="isRefresh">true:钉钉获取，false:从缓存获取</param>
        /// <returns></returns>
        public async Task<string> GetToken(string appkey, string appsecret, bool isRefresh = false)
        {
            try
            {
                ObjectCache _cache = MemoryCache.Default;
                if (_cache.Contains(appkey) && !isRefresh)
                {
                    return _cache.Get(appkey).ToString();
                }
                var client = new HttpClient();
                var request = await client.PostAsync($"{_constant.DING_URL_API}/oauth2/accessToken", JsonContent.Create(new
                {
                    appKey = appkey,
                    appSecret = appsecret,
                }));
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadFromJsonAsync<AccessToken>();
                    _cache.Set(appkey, response?.accessToken ?? string.Empty, new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds((response?.expireIn ?? 200) - 200),
                    });
                    return response?.accessToken ?? string.Empty;
                }
                else
                {
                    var error = await request.Content.ReadFromJsonAsync<DingBackError>();
                    return error?.message ?? string.Empty;
                }
            }
            catch (OApiException error)
            {
                error?.printStackTrace();
                return error?.Message ?? string.Empty;
            }
        }
    }
}
