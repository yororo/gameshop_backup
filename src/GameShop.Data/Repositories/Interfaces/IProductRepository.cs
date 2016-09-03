using GameShop.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(Guid id);
        IEnumerable<Product> FindProductsByName(string title);
        IEnumerable<Product> GetAllProducts();
        bool DeleteProduct(Guid id);
    }
}
