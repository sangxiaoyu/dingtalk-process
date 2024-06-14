using dingtalk_process.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace dingtalk_process
{
    public static class ServiceCollection
    {
        public static void DingTalkService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDingDingAuth, DingDingAuth>();
            services.Configure<Constant>(config.GetSection("Constant"));
        }
    }
}
