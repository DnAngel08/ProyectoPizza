﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BlazingPizza.Server.Models;

namespace BlazingPizza.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Host = CreateHostBuilder(args).Build();

            var ScopeFactory =
                Host.Services.GetRequiredService<IServiceScopeFactory>();
            using (var Scope = ScopeFactory.CreateScope())
            {
                var Context = Scope.ServiceProvider
                .GetRequiredService<PizzaStoreContext>();
                if (!Context.Specials.Any())
                {
                    SeedData.Initialize(Context);
                }
            }

            Host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
