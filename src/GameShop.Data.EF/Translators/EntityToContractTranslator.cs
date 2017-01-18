using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Entities;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Translators
{
    internal static class EntityToContractTranslator
    {
        // public static User ToUserContract(this Entities.User model)
        // {
        //     // Guard clause.
        //     if (model == null)
        //     {
        //         return null;
        //     }

        //     var user = new User();

        //     user.UserId = model.Id;
        //     user.Account = new Account()
        //     {
        //         Username = model.UserName,
        //         Email = model.Email,
        //         //EmailConfirmed = model.EmailConfirmed,
        //         PhoneNumber = model.PhoneNumber,
        //         //PhoneNumberConfirmed = model.PhoneNumberConfirmed,
        //         //CreatedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue),
        //         //ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue)
        //     };

        //     user.Profile = model.Profile.ToProfileContract();
        //     user.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
        //     user.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

        //     return user;
        // }

        //public static Account ToAccountContract(this EFEntities.Account model)
        //{
        //    // Guard clause.
        //    if (model == null)
        //    {
        //        return null;
        //    }

        //    var account = new Account();

        //    account.AccountId = model.AccountId;
        //    account.Username = model.Username;
        //    account.Email = model.Email;
        //    account.EmailVerified = model.EmailVerified;
        //    account.PasswordHash = model.PasswordHash;
        //    account.IsActive = model.IsActive;
        //    account.CreatedDate = DateTime.Now;
        //    account.ModifiedDate = DateTime.Now;

        //    return account;
        //}

        // public static Profile ToProfileContract(this EFEntities.Profile model)
        // {
        //     // Guard clause.
        //     if (model == null)
        //     {
        //         return null;
        //     }

        //     var profile = new Profile();

        //     profile.ProfileId = model.Id;
        //     profile.Name.Salutation = model.Salutation;
        //     profile.Name.FirstName = model.FirstName;
        //     profile.Name.MiddleName = model.MiddleName;
        //     profile.Name.LastName = model.LastName;
        //     profile.Name.Suffix = model.Suffix;
        //     profile.Gender = model.Gender;
        //     profile.CivilStatus = model.CivilStatus;
        //     profile.Birthday = model.Birthday;
        //     profile.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
        //     profile.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

        //     foreach (EFEntities.Address address in model.Addresses)
        //     {
        //         profile.Addresses.Add(address.ToProfileAddressContract());
        //     }

        //     foreach (EFEntities.ContactInformation contactInformation in model.ContactInformation)
        //     {
        //         profile.ContactInformation.Add(contactInformation.ToProfileContactInformationContract());
        //     }

        //     return profile;
        // }

        // public static Address ToProfileAddressContract(this EFEntities.Address model)
        // {
        //     // Guard clause.
        //     if (model == null)
        //     {
        //         return null;
        //     }

        //     var address = new Address();

        //     //address.AddressId = model.Id;
        //     address.Street1 = model.Street1;
        //     address.Street2 = model.Street2;
        //     address.Street3 = model.Street3;
        //     address.Barangay = model.Barangay;
        //     address.Municipality = model.Municipality;
        //     address.City = model.City;
        //     address.ZipCode = model.ZipCode;
        //     address.Province = model.Province;
        //     address.Region = model.Region;
        //     address.Country = model.Country;
        //     //address.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
        //     //address.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

        //     return address;
        // }

        // public static ContactInformation ToProfileContactInformationContract(this EFEntities.ContactInformation model)
        // {
        //     // Guard clause.
        //     if (model == null)
        //     {
        //         return null;
        //     }

        //     var contactInformation = new ContactInformation();

        //    // contactInformation.ContactInformationId = model.Id;
        //     contactInformation.Email = model.Email;
        //     contactInformation.PhoneNumber = model.MobileNumber;
        //     //contactInformation.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
        //     //contactInformation.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

        //     return contactInformation;
        // }

        public static Game ToGameContract(this EFEntities.Games.Game efGame)
        {
            Game gameContract = new Game();

            gameContract.CreatedDate = efGame.CreatedDate.Value;
            gameContract.Description = efGame.Description;
            gameContract.GamingPlatform = efGame.GamePlatform;
            gameContract.Genre = efGame.GameGenre;
            gameContract.Id = efGame.Id;
            gameContract.ModifiedDate = efGame.ModifiedDate.Value;
            gameContract.Name = efGame.Name;
            gameContract.ProductState = efGame.State;
            gameContract.SellingInformation = ToSellingInformation(efGame.SellingInformation);
            gameContract.TradingInformation = ToTradingInformation(efGame.TradingInformation);

            return gameContract;
        }
        
        public static List<Game> ToGameContracts(this List<EFEntities.Games.Game> games)
        {
            var gameContracts = new List<Game>();

            foreach (EFEntities.Games.Game game in games)
            {
                gameContracts.Add(game.ToGameContract());
            }

            return gameContracts;
        }

        public static Advertisement<Game> ToAdvertisementContract(this EFEntities.Games.GameAdvertisement efAd)
        {
            GameAdvertisement gameAdContract = new GameAdvertisement();

            gameAdContract.CreatedDate = efAd.CreatedDate.Value;
            gameAdContract.Description = efAd.Description;
            gameAdContract.FriendlyId = efAd.FriendlyId;
            gameAdContract.Id = efAd.Id;
            //gameAdvertisement.MeetupInformation = efAd.MeetupInformation;
            gameAdContract.ModifiedDate = efAd.ModifiedDate.Value;
            //gameAdvertisement.Owner = efAd.Owner;
            
            foreach(EFEntities.Games.Game efGame in efAd.Games)
            {
                gameAdContract.Products.Add(ToGameContract(efGame));
            }

            gameAdContract.State = efAd.State;
            gameAdContract.Title = efAd.Title;

            return gameAdContract;
        }

        public static SellingInformation ToSellingInformation(this EFEntities.SellingInformation efSellingInfo)
        {
            SellingInformation sellingInfoContract = new SellingInformation();

            sellingInfoContract.CreatedDate = efSellingInfo.CreatedDate.Value;
            sellingInfoContract.Currency = efSellingInfo.Currency;
            sellingInfoContract.Id = efSellingInfo.Id;
            sellingInfoContract.ModifiedDate = efSellingInfo.ModifiedDate.Value;
            sellingInfoContract.ReasonForSelling = efSellingInfo.ReasonForSelling;
            sellingInfoContract.SellingPrice = efSellingInfo.SellingPrice;

            return sellingInfoContract;
        }

        public static TradingInformation ToTradingInformation(this EFEntities.TradingInformation efTradingInfo)
        {
            TradingInformation tradingInfoContract = new TradingInformation();

            tradingInfoContract.CashAmountWillingToAdd = efTradingInfo.CashAmountToAdd;
            tradingInfoContract.CreatedDate = efTradingInfo.CreatedDate.Value;
            tradingInfoContract.Currency = efTradingInfo.Currency;
            tradingInfoContract.Id = efTradingInfo.Id;
            tradingInfoContract.IsOwnerWillingToAddCash = efTradingInfo.IsOwnerWillingToAddCash;
            tradingInfoContract.IsOwnerWillingToReceiveCash = efTradingInfo.IsOwnerWillingToReceiveCash;
            tradingInfoContract.ModifiedDate = efTradingInfo.ModifiedDate.Value;
            tradingInfoContract.ReasonForTrading = efTradingInfo.ReasonForSelling;
            tradingInfoContract.TradeNotes = efTradingInfo.TradeNotes;
            tradingInfoContract.TradingPrice = efTradingInfo.TradingPrice;

            return tradingInfoContract;
        }
    }
}
