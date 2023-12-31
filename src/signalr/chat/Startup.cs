// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Azure.SignalR.Samples.ChatRoom
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration configuration, IHostEnvironment environment) 
        {
            config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var signalR = services.AddSignalR();
            if (bool.Parse(config["Azure:SignalR:Enabled"]))
            {
                signalR.AddAzureSignalR();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatSampleHub>("/chat");
            });
        }
    }
}
