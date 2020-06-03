using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RepositoryPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var host = CreateHostBuilder(args).Build(); //.Run();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<ForumContext>();
            //        context.Database.Migrate();

            //        var context2 = services.GetRequiredService<MasterContext>();
            //        context2.Database.Migrate();
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //}
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
