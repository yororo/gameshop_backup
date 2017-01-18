using GameShop.Data.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Migrations
{
    internal class GameShopContextFactory : IDbContextFactory<GameShopContext>
    {
        public string ConnectionString
        {
            get
            {
                try
                {
                    //return File.ReadAllText("ConnectionString.txt");
                    Console.WriteLine("Please enter connection string:");
                    return Console.ReadLine();
                }
                catch(Exception ex)
                {     
                    File.Create(Directory.GetCurrentDirectory());
                                   
                    throw new Exception($"Please enter the connection string in the ConnectionString.txt file found in { Directory.GetCurrentDirectory() }.", ex);
                }
            }
        }

        public GameShopContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<GameShopContext>();

            builder.UseMySql(ConnectionString);

            return new GameShopContext(builder.Options);
        }
    }
}
