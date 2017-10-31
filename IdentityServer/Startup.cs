using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer(opt => opt.IssuerUri = "http://identityserver")
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiResources(Configuration.GetApiResources())
                .AddInMemoryClients(Configuration.GetClients())
                .AddCorsPolicyService<InMemoryCorsPolicyService>()
                .AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Information);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseIdentityServer();
        }
    }
}
