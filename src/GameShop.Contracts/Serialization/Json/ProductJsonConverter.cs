using System;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;
using Newtonsoft.Json.Linq;

namespace GameShop.Contracts.Serialization.Json
{
    /// <summary>
    /// This is used to properly deserialize JSON of types deriving from the Product class.
    /// </summary>
    public class ProductJsonConverter : BaseJsonConverter<Product>
    {
        protected override Product Create(Type type, JObject json)
        {
            ProductCategory category = (ProductCategory)json.Value<int>("Category");

            // For new products, we need to add the new product category in this switch statement:

            switch(category)
            {
                case ProductCategory.Games:
                    return new Game();

                case ProductCategory.GameConsoles:
                    return new GameConsole();

                case ProductCategory.General:
                    return new GeneralProduct();

                default:
                    return null;
            }
        }
    }
}