using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Entities;
using GameShop.Data.EF.Entities.Games;

namespace GameShop.Contracts.Entities
{
    /// <summary>
    /// Translators for contract classes in the games category.
    /// </summary>
    internal static class ContractTranslators
    {
        #region Advertisement

        public static EfAdvertisement ToEntity(this Advertisement advertisementContract)
        {
            if (advertisementContract == null)
            {
                return null;
            }

            var advertisementEntity = new EfAdvertisement();

            advertisementEntity.CreatedDate = advertisementContract.CreatedDate;
            advertisementEntity.Description = advertisementContract.Description;
            advertisementEntity.FriendlyId = advertisementContract.FriendlyId;
            advertisementEntity.Id = advertisementContract.Id;
            advertisementEntity.MeetupInformation = advertisementContract.MeetupInformation.ToEntity(advertisementEntity);
            advertisementEntity.ModifiedDate = advertisementEntity.ModifiedDate;
            //advertisementEntity.Owner = advertisementEntity.Owner;
            advertisementEntity.State = advertisementContract.State;
            advertisementEntity.Title = advertisementContract.Title;

            // Products
            // foreach(Product productContract in advertisementContract.Products)
            // {
            //     advertisementEntity.AdvertisementProducts.Add(new EfAdvertisementProducts()
            //     {
            //         AdvertisementId = advertisementEntity.Id,
            //         Advertisement = advertisementEntity,
            //         ProductId = productContract.Id,
            //         Product = productContract.ToEntity(advertisementEntity)
            //     });
            // }

            return advertisementEntity;
        }
        
        public static List<EfAdvertisement> ToEntities(this IEnumerable<Advertisement> advertisementContracts)
        {
            if(advertisementContracts == null)
            {
                return new List<EfAdvertisement>();
            }

            var advertisementEntities = new List<EfAdvertisement>(advertisementContracts.Count());

            foreach(Advertisement advertisementContract in advertisementContracts)
            {
                advertisementEntities.Add(advertisementContract.ToEntity());
            }

            return advertisementEntities;
        }

        #endregion Advertisement
        
        #region Product
            
        public static EfProduct ToEntity(this Product productContract, EfAdvertisement parentEntity = null)
        {
            if(productContract == null)
            {
                return null;
            }

            // if(parentEntity != null)
            // {
            //     productEntity.AdvertisementProducts.Add(new EfAdvertisementProducts()
            //     {
            //         AdvertisementId = parentEntity.Id,
            //         Advertisement = parentEntity,
            //         ProductId = productEntity.Id,
            //         Product = productEntity
            //     });
            // }

            switch(productContract.Category)
            {
                case ProductCategory.Games:
                    return ToEntity((Game)productContract);

                case ProductCategory.GameConsoles:
                    return null; // Not yet implemented.

                default:
                    var productEntity = new EfProduct();
                    productEntity.CreatedDate = productContract.CreatedDate;
                    productEntity.Description = productContract.Description;
                    productEntity.Id = productContract.Id;
                    productEntity.ModifiedDate = productContract.ModifiedDate;
                    productEntity.Name = productContract.Name;
                    productEntity.ProductType = productContract.ProductType;
                    productEntity.SellingInformation = productContract.SellingInformation.ToEntity(productEntity);
                    productEntity.TradingInformation = productContract.TradingInformation.ToEntity(productEntity);
                    return productEntity;
            }
        }

        public static List<EfProduct> ToEntities(this IEnumerable<Product> productContracts, EfAdvertisement parentEntity = null)
        {
            if(productContracts == null)
            {
                return new List<EfProduct>();
            }

            var productEntities = new List<EfProduct>(productContracts.Count());

            foreach(Product productContract in productContracts)
            {
                productEntities.Add(productContract.ToEntity(parentEntity));
            }

            return productEntities;
        }

        #endregion Product

        #region Game
            
        public static EfGame ToEntity(this Game gameContract, EfAdvertisement parentEntity = null)
        {
            // Guard clause.
            if (gameContract == null)
            {
                return null;
            }

            
            var gameEntity = new EfGame();
            gameEntity.CreatedDate = gameContract.CreatedDate;
            gameEntity.Description = gameContract.Description;
            gameEntity.GamingPlatform = gameContract.GamingPlatform;
            gameEntity.Genre = gameContract.Genre;
            gameEntity.Id = gameContract.Id;
            gameEntity.ModifiedDate = gameContract.ModifiedDate;
            gameEntity.Name = gameContract.Name;
            gameEntity.ProductType = gameContract.ProductType;
            gameEntity.SellingInformation = gameContract.SellingInformation.ToEntity(gameEntity);
            gameEntity.TradingInformation = gameContract.TradingInformation.ToEntity(gameEntity);

            // if(parentEntity != null)
            // {
            //     gameEntity.AdvertisementProducts.Add(new EfAdvertisementProducts()
            //     {
            //         AdvertisementId = parentEntity.Id,
            //         Advertisement = parentEntity,
            //         ProductId = gameEntity.Id,
            //         Product = gameEntity
            //     });
            // }

            return gameEntity;
        }

        public static List<EfGame> ToEntities(IEnumerable<Game> gameContracts, EfAdvertisement parentEntity = null)
        {
            if(gameContracts == null)
            {
                return new List<EfGame>();
            }

            var gameEntities = new List<EfGame>(gameContracts.Count());

            foreach(Game gameContract in gameContracts)
            {
                gameEntities.Add(gameContract.ToEntity(parentEntity));
            }

            return gameEntities;
        }

        #endregion Game

        #region SellingInformation
            
        public static EfSellingInformation ToEntity(this SellingInformation sellingInfoContract, EfProduct parentEntity)
        {
            // Guard clause.
            if (sellingInfoContract == null)
            {
                return null;
            }

            var sellingInfoEntity = new EfSellingInformation();

            sellingInfoEntity.CreatedDate = sellingInfoContract.CreatedDate;
            sellingInfoEntity.Currency = sellingInfoContract.Currency;
            sellingInfoEntity.Id = sellingInfoContract.Id;
            sellingInfoEntity.ModifiedDate = sellingInfoContract.ModifiedDate;
            sellingInfoEntity.ReasonForSelling = sellingInfoContract.ReasonForSelling;
            sellingInfoEntity.SellingPrice = sellingInfoContract.SellingPrice;

            if(parentEntity != null)
            {
                sellingInfoEntity.ProductId = parentEntity.Id;
                sellingInfoEntity.Product = parentEntity;
            }

            return sellingInfoEntity;
        }

        #endregion SellingInformation

        #region TradingInformation

        public static EfTradingInformation ToEntity(this TradingInformation tradingInfoContract, EfProduct parentEntity)
        {
            // Guard clause.
            if (tradingInfoContract == null)
            {
                return null;
            }

            var tradingInfoEntity = new EfTradingInformation();

            tradingInfoEntity.Currency = tradingInfoContract.Currency;
            tradingInfoEntity.IsOwnerWillingToAddCash = tradingInfoContract.IsOwnerWillingToAddCash;
            tradingInfoEntity.CashAmountToAdd = tradingInfoContract.CashAmountToAdd;
            tradingInfoEntity.IsOwnerWillingToReceiveCash = tradingInfoContract.IsOwnerWillingToReceiveCash;
            tradingInfoEntity.CashAmountToReceive = tradingInfoContract.CashAmountToReceive;
            tradingInfoEntity.ReasonForTrading = tradingInfoContract.ReasonForTrading;
            tradingInfoEntity.TradeNotes = tradingInfoContract.TradeNotes;
            tradingInfoEntity.TradingPrice = tradingInfoContract.TradingPrice;
            tradingInfoEntity.CreatedDate = tradingInfoContract.CreatedDate;
            tradingInfoEntity.ModifiedDate = tradingInfoContract.ModifiedDate;

            if(parentEntity != null)
            {
                tradingInfoEntity.ProductId = parentEntity.Id;
                tradingInfoEntity.Product = parentEntity;
            }

            return tradingInfoEntity;
        }

        #endregion TradingInformation

        #region MeetupInformation
            
        public static EfMeetupInformation ToEntity(this MeetupInformation meetupInfoContract, EfAdvertisement parentEntity)
        {
            if(meetupInfoContract == null)
            {
                return null;
            }

            var meetupInfoEntity = new EfMeetupInformation();
            // Assign ID to meetup information.
            meetupInfoEntity.Id = Guid.NewGuid();
            meetupInfoEntity.ContactNumber = meetupInfoContract.ContactNumber;
            meetupInfoEntity.Notes = meetupInfoContract.Notes;

            foreach(MeetupLocation meetupLocationContract in meetupInfoContract.MeetupLocations)
            {
                meetupInfoEntity.MeetupLocations.Add(meetupLocationContract.ToEntity(meetupInfoEntity));
            }

            if(parentEntity != null)
            {
                meetupInfoEntity.AdvertisementId = parentEntity.Id;
                meetupInfoEntity.Advertisement = parentEntity;
            }

            return meetupInfoEntity;
        }

        #endregion MeetupInformation

        #region MeetupLocation
            
        public static EfMeetupLocation ToEntity(this MeetupLocation meetupLocationContract, EfMeetupInformation parentEntity)
        {
            if(meetupLocationContract == null)
            {
                return null;
            }

            var meetupLocationEntity = new EfMeetupLocation();
            // Assign ID to meetup information.
            meetupLocationEntity.Id = Guid.NewGuid();
            meetupLocationEntity.Barangay = meetupLocationContract.Barangay;
            meetupLocationEntity.City = meetupLocationContract.City;   
            meetupLocationEntity.Country = meetupLocationContract.Country;
            meetupLocationEntity.Municipality = meetupLocationContract.Municipality;
            meetupLocationEntity.MeetupNotes = meetupLocationContract.MeetupNotes;
            meetupLocationEntity.Province = meetupLocationContract.Province;
            meetupLocationEntity.Region = meetupLocationContract.Region;
            meetupLocationEntity.Street1 = meetupLocationContract.Street1;
            meetupLocationEntity.Street2 = meetupLocationContract.Street2;
            meetupLocationEntity.Street3 = meetupLocationContract.Street3;
            meetupLocationEntity.ZipCode = meetupLocationContract.ZipCode;

            if(parentEntity != null)
            {
                meetupLocationEntity.MeetupInformationId = parentEntity.Id;
                meetupLocationEntity.MeetupInformation = parentEntity;
            }

            return meetupLocationEntity;
        }
        
        #endregion MeetupLocation
    }
}

