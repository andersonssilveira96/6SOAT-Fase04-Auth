using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Lambda.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TechChallenge.Authentication.Model;
using TechChallenge.Authentication.Service;


namespace TechChallenge.Authentication
{
    [LambdaStartup]
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.Configure<OptionsDto>(configuration);

            services.AddAuthentication();

            services.AddAuthorization();

            services.AddCognitoIdentity();

            services.AddScoped<ICognitoService, CognitoService>();

            services.AddScoped<ICognitoService>(x =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var opt = serviceProvider.GetRequiredService<IOptions<OptionsDto>>();

                var provider = new AmazonCognitoIdentityProviderClient(RegionEndpoint.GetBySystemName(opt.Value.Region));
                var client = new AmazonCognitoIdentityProviderClient();

                return new CognitoService(opt, client, provider);
            });
        }      
    }
}
