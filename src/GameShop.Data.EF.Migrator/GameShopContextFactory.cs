using GameShop.Data.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Migrator
{
    public class GameShopContextFactory : IDbContextFactory<GameShopContext>
    {
        public string ConnectionString
        {
            get
            {
                try
                {
                    return File.ReadAllText("ConnectionString.txt");
                }
                catch(Exception ex)
                {
                    throw new Exception($"Please create a ConnectionString.txt file on { Directory.GetCurrentDirectory() } containing the connection string for migration.", ex);
                }
            }
        }

        public GameShopContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<GameShopContext>();

            builder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("GameShop.Data.EF.Migrations"));

            return new GameShopContext(builder.Options);
        }
    }
}
