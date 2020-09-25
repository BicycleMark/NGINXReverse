using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloAspNetCore3.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // TODO Understand this. The dafault Template has Use HttpsRedirection() set to true.
            //app.UseHttpsRedirection();
            // This is what you do when you are using load balancers and proxy servers 
            // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-3.1
           //// ForwardedHeadersOptions options = new ForwardedHeadersOptions();
          //  options.ForwardedHeaders = ForwardedHeaders.XForwardedFor| ForwardedHeaders.XForwardedProto;
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                /*
                X-Forwarded-For:	
                    Holds information about the client that initiated the request and subsequent proxies in a chain of proxies. 
                This parameter may contain IP addresses (and, optionally, port numbers). In a chain of proxy servers, 
                the first parameter indicates the client where the request was first made. 
                Subsequent proxy identifiers follow. The last proxy in the chain isn't in the list of parameters. 
                The last proxy's IP address, and optionally a port number, are available as the remote IP address at the transport layer.

                X-Forwarded-For:
                	Holds information about the client that initiated the request and subsequent proxies in a chain of proxies. 
                This parameter may contain IP addresses (and, optionally, port numbers). 
                In a chain of proxy servers, the first parameter indicates the client where the request was first made. 
                Subsequent proxy identifiers follow. The last proxy in the chain isn't in the list of parameters. 
                The last proxy's IP address, and optionally a port number, are available as the remote IP address at the transport layer.

                */
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

           
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
