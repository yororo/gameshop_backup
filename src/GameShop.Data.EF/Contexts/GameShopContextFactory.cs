using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Contexts
{
    internal class GameShopContextFactory : IDbContextFactory<GameShopContext>
    {
        public GameShopContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<GameShopContext>();
            builder.UseSqlServer("Server=jeyjeyemem-pc\\sqlexpress;Database=gsph;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new GameShopContext(builder.Options);
        }
    }
}
