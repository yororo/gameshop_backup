﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GameShop.Authorization 
{
    public class Program 
    {
        public static void Main(string[] args) 
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:5000")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
