using System;
using GameShop.Contracts.Enumerations;

namespace GameShop.Contracts.Entities.Products
{
    public class GeneralProduct : Product
    {
        public override ProductCategory Category
        {
            get
            {
                return ProductCategory.General;
            }
        }
    }
}