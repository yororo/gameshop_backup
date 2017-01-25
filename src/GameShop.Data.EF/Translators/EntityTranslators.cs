using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Entities;
using GameShop.Data.EF.Entities.Products;

namespace GameShop.Data.EF
{
    /// <summary>
    /// Translators for entity classes.
    /// </summary>
    internal static class EntityTranslators
    {
        #region Advertisements
            
        public static Advertisement ToContract(this EfAdvertisement advertisementEntity)
        {
            // Guard clause.
            if (advertisementEntity == null)
            {
                return null;
            }

            var advertisementContract = new Advertisement();

            advertisementContract.CreatedDate = advertisementEntity.CreatedDate.Value;
            advertisementContract.Description = advertisementEntity.Description;
            advertisementContract.FriendlyId = advertisementEntity.FriendlyId;
            advertisementContract.Id = advertisementEntity.Id;
            advertisementContract.MeetupInformation = advertisementEntity.MeetupInformation.ToContract();
            advertisementContract.ModifiedDate = advertisementEntity.ModifiedDate.Value;
            //advertisementContract.Owner = advertisementEntity.Owner;
            advertisementContract.State = advertisementEntity.State;
            advertisementContract.Title = advertisementEntity.Title;

            // Populate products
            foreach(EfAdvertisementProducts advertisementProductsEntity in advertisementEntity.AdvertisementProducts)
            {
                advertisementContract.Products.Add(advertisementProductsEntity.Product.ToContract());
            }

            return advertisementContract;
        }

        public static List<Advertisement> ToContracts(this IEnumerable<EfAdvertisement> advertisementEntities)
        {
            if(advertisementEntities == null)
            {
                // Return empty list.
                return new List<Advertisement>();
            }

            var advertisementContracts = new List<Advertisement>(advertisementEntities.Count());

            foreach(EfAdvertisement advertisementEntity in advertisementEntities)
            {
                advertisementContracts.Add(advertisementEntity.ToContract());
            }

            return advertisementContracts;
        }

        #endregion Advertisements

        #region Product
            
        public static Product ToContract(this EfProduct productEntity)
        {
            if(productEntity == null)
            {
                return null;
            }

            switch(productEntity.Category)
            {
                case ProductCategory.Games:
                    return ToContract((EfGame)productEntity);

                case ProductCategory.GameConsoles:
                    return ToContract((EfGame)productEntity);

                case ProductCategory.General: // General
                    var productContract = new GeneralProduct();
                    productContract.CreatedDate = productEntity.CreatedDate.Value;
                    productContract.Description = productEntity.Description;
                    productContract.Id = productEntity.Id;
                    productContract.ModifiedDate = productEntity.ModifiedDate.Value;
                    productContract.Name = productEntity.Name;
                    productContract.ProductType = productEntity.ProductType;
                    productContract.SellingInformation = productEntity.SellingInformation.ToContract();
                    productContract.TradingInformation = productEntity.TradingInformation.ToContract();
                    return productContract;

                default:
                    throw new NotSupportedException($"Product category '{productEntity.Category}' is not supported.");
            }
        }

        public static List<Product> ToContracts(this IEnumerable<EfProduct> productEntities)
        {
            if (productEntities == null)
            {
                // Return empty list.
                return new List<Product>();
            }

            var productContracts = new List<Product>(productEntities.Count());

            foreach (EfProduct productEntity in productEntities)
            {
                productContracts.Add(productEntity.ToContract());
            }

            return productContracts;
        }

        #endregion Product

        #region Game
        
        public static Game ToContract(this EfGame gameEntity)
        {
            if (gameEntity == null)
            {
                return null;
            }

            var gameContract = new Game();
            gameContract.CreatedDate = gameEntity.CreatedDate.Value;
            gameContract.Description = gameEntity.Description;
            gameContract.GamingPlatform = gameEntity.GamingPlatform;
            gameContract.Genre = gameEntity.Genre;
            gameContract.Id = gameEntity.Id;
            gameContract.ModifiedDate = gameEntity.ModifiedDate.Value;
            gameContract.Name = gameEntity.Name;
            gameContract.ProductType = gameEntity.ProductType;
            gameContract.SellingInformation = gameEntity.SellingInformation.ToContract();
            gameContract.TradingInformation = gameEntity.TradingInformation.ToContract();

            return gameContract;
        }

        public static List<Game> ToContracts(this IEnumerable<EfGame> gameEntities)
        {
            if (gameEntities == null)
            {
                return null;
            }

            var gameContracts = new List<Game>(gameEntities.Count());

            foreach (EfGame gameEntity in gameEntities)
            {
                gameContracts.Add(gameEntity.ToContract());
            }

            return gameContracts;
        }

        #endregion Game
        
        #region GameConsole
        
        public static GameConsole ToContract(this EfGameConsole gameConsoleEntity)
        {
            if (gameConsoleEntity == null)
            {
                return null;
            }

            var gameConsoleContract = new GameConsole();
            gameConsoleContract.ConsolePlatform = gameConsoleEntity.ConsolePlatform;
            gameConsoleContract.CreatedDate = gameConsoleEntity.CreatedDate.Value;
            gameConsoleContract.Description = gameConsoleEntity.Description;
            gameConsoleContract.Id = gameConsoleEntity.Id;
            gameConsoleContract.ModifiedDate = gameConsoleEntity.ModifiedDate.Value;
            gameConsoleContract.Name = gameConsoleEntity.Name;
            gameConsoleContract.ProductType = gameConsoleEntity.ProductType;
            gameConsoleContract.SellingInformation = gameConsoleEntity.SellingInformation.ToContract();
            gameConsoleContract.TradingInformation = gameConsoleEntity.TradingInformation.ToContract();

            return gameConsoleContract;
        }

        public static List<GameConsole> ToContracts(this IEnumerable<EfGameConsole> gameConsoleEntities)
        {
            if (gameConsoleEntities == null)
            {
                return null;
            }

            var gameConsoleContracts = new List<GameConsole>(gameConsoleEntities.Count());

            foreach (EfGameConsole gameEntity in gameConsoleEntities)
            {
                gameConsoleContracts.Add(gameEntity.ToContract());
            }

            return gameConsoleContracts;
        }

        #endregion GameConsole

        #region GameSellingInformation
            
        public static SellingInformation ToContract(this EfSellingInformation sellingInfoEntity)
        {
            if (sellingInfoEntity == null)
            {
                return null;
            }

            var sellingInfoContract = new SellingInformation();

            sellingInfoContract.CreatedDate = sellingInfoEntity.CreatedDate.Value;
            sellingInfoContract.Currency = sellingInfoEntity.Currency;
            //sellingInfoContract.Id = sellingInfoEntity.Id;
            sellingInfoContract.ModifiedDate = sellingInfoEntity.ModifiedDate.Value;
            sellingInfoContract.ReasonForSelling = sellingInfoEntity.ReasonForSelling;
            sellingInfoContract.SellingPrice = sellingInfoEntity.SellingPrice;

            return sellingInfoContract;
        }

        #endregion GameSellingInformation

        #region GameTradingInformation
        public static TradingInformation ToContract(this EfTradingInformation tradingInfoEntity)
        {
            if (tradingInfoEntity == null)
            {
                return null;
            }

            TradingInformation tradingInfoContract = new TradingInformation();

            tradingInfoContract.CashAmountToAdd = tradingInfoEntity.CashAmountToAdd;
            tradingInfoContract.CashAmountToReceive = tradingInfoEntity.CashAmountToReceive;
            tradingInfoContract.CreatedDate = tradingInfoEntity.CreatedDate.Value;
            tradingInfoContract.Currency = tradingInfoEntity.Currency;
            //tradingInfoContract.Id = tradingInfoEntity.Id;
            tradingInfoContract.IsOwnerWillingToAddCash = tradingInfoEntity.IsOwnerWillingToAddCash;
            tradingInfoContract.IsOwnerWillingToReceiveCash = tradingInfoEntity.IsOwnerWillingToReceiveCash;
            tradingInfoContract.ModifiedDate = tradingInfoEntity.ModifiedDate.Value;
            tradingInfoContract.ReasonForTrading = tradingInfoEntity.ReasonForTrading;
            tradingInfoContract.TradeNotes = tradingInfoEntity.TradeNotes;
            tradingInfoContract.TradingPrice = tradingInfoEntity.TradingPrice;

            return tradingInfoContract;
        }

        #endregion GameTradingInformation

        #region MeetupInformation
            
        public static MeetupInformation ToContract(this EfMeetupInformation meetupInfoEntity)
        {
            if(meetupInfoEntity == null)
            {
                return null;
            }

            var meetupInfoContract = new MeetupInformation();
            meetupInfoContract.ContactNumber = meetupInfoEntity.ContactNumber;
            meetupInfoContract.Notes = meetupInfoEntity.Notes;

            foreach(EfMeetupLocation meetupLocationEntity in meetupInfoEntity.MeetupLocations)
            {
                meetupInfoContract.MeetupLocations.Add(meetupLocationEntity.ToContract());
            }

            return meetupInfoContract;
        }

        #endregion MeetupInformation

        #region MeetupLocation
            
        public static MeetupLocation ToContract(this EfMeetupLocation meetupLocationEntity)
        {
            if(meetupLocationEntity == null)
            {
                return null;
            }

            var meetupLocationContract = new MeetupLocation();
            meetupLocationContract.Barangay = meetupLocationEntity.Barangay;
            meetupLocationContract.City = meetupLocationEntity.City;   
            meetupLocationContract.Country = meetupLocationEntity.Country;
            meetupLocationContract.Municipality = meetupLocationEntity.Municipality;
            meetupLocationContract.MeetupNotes = meetupLocationEntity.MeetupNotes;
            meetupLocationContract.Province = meetupLocationEntity.Province;
            meetupLocationContract.Region = meetupLocationEntity.Region;
            meetupLocationContract.Street1 = meetupLocationEntity.Street1;
            meetupLocationContract.Street2 = meetupLocationEntity.Street2;
            meetupLocationContract.Street3 = meetupLocationEntity.Street3;
            meetupLocationContract.ZipCode = meetupLocationEntity.ZipCode;

            return meetupLocationContract;
        } 

        #endregion MeetupLocation
    }
}

// Example translator class for contracts in the consoles category.

// namespace GameShop.Data.EF.Entities.Consoles
// {
//     /// <summary>
//     /// Translators for contract classes in the consoles category.
//     /// </summary>
//     internal class ConsolesContractTranslator
//     {
//         // ToEntity() methods
//     }
// }