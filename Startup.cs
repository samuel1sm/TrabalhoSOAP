using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoapApi.Controllers;
using SoapApi.Interfaces;
using SoapCore;

namespace SoapApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUsuarioService, Service>();
            services.AddSingleton<IMusicService, Service>();
            services.AddSingleton<IPlaylistService, Service>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSoapEndpoint<IUsuarioService>($"/UserService.asmx", new SoapEncoderOptions());
            app.UseSoapEndpoint<IMusicService>($"/MusicService.asmx", new SoapEncoderOptions());
            app.UseSoapEndpoint<IPlaylistService>($"/PlaylistService.asmx", new SoapEncoderOptions());


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
