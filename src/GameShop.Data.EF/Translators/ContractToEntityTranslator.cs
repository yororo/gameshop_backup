using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Entities;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Translators
{
    internal static class ContractToEntityTranslator
    {
        // public static EFEntities.User ToUserEntity(this User user)
        // {
        //     // Guard clause.
        //     if(user == null)
        //     {
        //         return null;
        //     }

        //     var model = new EFEntities.User();

        //     model.Id = user.UserId;
        //     model.UserName = user.Account.Username;
        //     model.Email = user.Account.Email;
        //     //model.EmailConfirmed = user.Account.EmailConfirmed;
        //     model.PhoneNumber = user.Account.PhoneNumber;
        //     //model.PhoneNumberConfirmed = user.Account.PhoneNumberConfirmed;
        //     //model.Account = user.Account.ToAccountEntity(model);
        //     model.Profile = user.Profile.ToProfileEntity(model);
        //     model.CreatedDate = user.CreatedDate;
        //     model.ModifiedDate = user.ModifiedDate;

        //     return model;
        // }

        //public static EFEntities.Account ToAccountEntity(this Account account, EFEntities.User parentUser = null)
        //{
        //    // Guard clause.
        //    if (account == null)
        //    {
        //        return null;
        //    }

        //    var model = new EFEntities.Account();

        //    model.AccountId = account.AccountId;
        //    model.Username = account.Username;
        //    model.Email = account.Email;
        //    model.EmailVerified = account.EmailVerified;
        //    model.PasswordHash = account.PasswordHash;
        //    model.IsActive = account.IsActive;
        //    model.CreatedDate = account.CreatedDate;
        //    model.ModifiedDate = account.ModifiedDate;

        //    if (parentUser != null)
        //    {
        //        model.UserId = parentUser.UserId;
        //        model.User = parentUser;
        //    }

        //    return model;
        //}

        // public static EFEntities.Profile ToProfileEntity(this Profile profile, EFEntities.User parentUser = null)
        // {
        //     // Guard clause.
        //     if (profile == null)
        //     {
        //         return null;
        //     }

        //     var model = new EFEntities.Profile();

        //     model.Id = profile.ProfileId;
        //     model.Salutation = profile.Name.Salutation;
        //     model.FirstName = profile.Name.FirstName;
        //     model.MiddleName = profile.Name.MiddleName;
        //     model.LastName = profile.Name.LastName;
        //     model.Suffix = profile.Name.Suffix;
        //     model.Gender = profile.Gender;
        //     model.CivilStatus = profile.CivilStatus;
        //     model.Birthday = profile.Birthday;
        //     model.CreatedDate = profile.CreatedDate;
        //     model.ModifiedDate = profile.ModifiedDate;
        //     model.Addresses = new List<EFEntities.ProfileAddress>();
        //     model.ContactInformation = new List<EFEntities.ProfileContactInformation>();

        //     foreach(Address address in profile.Addresses)
        //     {
        //         model.Addresses.Add(address.ToProfileAddressEntity(model));
        //     }

        //     foreach(ContactInformation contactInformation in profile.ContactInformation)
        //     {
        //         model.ContactInformation.Add(contactInformation.ToProfileContactInformationEntity(model));
        //     }

        //     if(parentUser != null)
        //     {
        //         model.UserId = parentUser.Id;
        //         model.User = parentUser;
        //     }

        //     return model;
        // }

        // public static EFEntities.ProfileAddress ToProfileAddressEntity(this Address address, EFEntities.Profile parentProfile = null)
        // {
        //     // Guard clause.
        //     if (address == null)
        //     {
        //         return null;
        //     }

        //     var model = new EFEntities.ProfileAddress();

        //    // model.Id = address.AddressId;
        //     model.Street1 = address.Street1;
        //     model.Street2 = address.Street2;
        //     model.Street3 = address.Street3;
        //     model.Barangay = address.Barangay;
        //     model.Municipality = address.Municipality;
        //     model.City = address.City;
        //     model.ZipCode = address.ZipCode;
        //     model.Province = address.Province;
        //     model.Region = address.Region;
        //     model.Country = address.Country;
        //     //model.CreatedDate = address.CreatedDate;
        //     //model.ModifiedDate = address.ModifiedDate;

        //     if (parentProfile != null)
        //     {
        //         model.ProfileId = parentProfile.Id;
        //         model.Profile = parentProfile;
        //     }

        //     return model;
        // }

        // public static EFEntities.ProfileContactInformation ToProfileContactInformationEntity(this ContactInformation contactInformation,
        //                                                                                         EFEntities.Profile parentProfile = null)
        // {
        //     // Guard clause.
        //     if (contactInformation == null)
        //     {
        //         return null;
        //     }

        //     var model = new EFEntities.ProfileContactInformation();

        //     //model.Id = contactInformation.ContactInformationId;
        //     model.Email = contactInformation.Email;
        //     model.MobileNumber = contactInformation.PhoneNumber;
        //     //model.CreatedDate = contactInformation.CreatedDate;
        //     //model.ModifiedDate = contactInformation.ModifiedDate;

        //     if (parentProfile != null)
        //     {
        //         model.ProfileId = parentProfile.Id;
        //         model.Profile = parentProfile;
        //     }

        //     return model;
        // }

        public static EFEntities.Games.GameAdvertisement ToGameAdvertisementEntity(this GameAdvertisement advertisement)
        {
            // Guard clause.
            if (advertisement == null)
            {
                return null;
            }

            var model = new EFEntities.Games.GameAdvertisement();

            model.Id = advertisement.Id;
            model.CreatedDate = advertisement.CreatedDate;
            model.Description = advertisement.Description;
            model.FriendlyId = advertisement.FriendlyId;
            model.ModifiedDate = advertisement.ModifiedDate;
            model.State = advertisement.State;
            model.Title = advertisement.Title;
            
            foreach(var game in advertisement.Products)
            {
                model.Games.Add(game.ToGameEntity());
            }

            return model;
        }

        public static EFEntities.Games.GameSellingInformation ToGameSellingInformationEntity(this SellingInformation sellingInformation)
        {
            // Guard clause.
            if (sellingInformation == null)
            {
                return null;
            }

            var model = new EFEntities.Games.GameSellingInformation();

            model.Currency = sellingInformation.Currency;
            model.ReasonForSelling = sellingInformation.ReasonForSelling;
            model.SellingPrice = sellingInformation.SellingPrice;
            model.CreatedDate = sellingInformation.CreatedDate;
            model.ModifiedDate = sellingInformation.ModifiedDate;

            return model;
        }

        public static EFEntities.Games.GameTradingInformation ToGameTradingInformationEntity(this TradingInformation tradingInformation)
        {
            // Guard clause.
            if (tradingInformation == null)
            {
                return null;
            }

            var model = new EFEntities.Games.GameTradingInformation();

            model.CashAmountToAdd = tradingInformation.CashAmountWillingToAdd;
            model.Currency = tradingInformation.Currency;
            model.IsOwnerWillingToAddCash = tradingInformation.IsOwnerWillingToAddCash;
            model.IsOwnerWillingToReceiveCash = tradingInformation.IsOwnerWillingToReceiveCash;
            model.ReasonForSelling = tradingInformation.ReasonForTrading;
            model.TradeNotes = tradingInformation.TradeNotes;
            model.TradingPrice = tradingInformation.TradingPrice;
            model.CreatedDate = tradingInformation.CreatedDate;
            model.ModifiedDate = tradingInformation.ModifiedDate;

            return model;
        }

        #region Product Translations

        public static EFEntities.Games.Game ToGameEntity(this Game game)
        {
            // Guard clause.
            if (game == null)
            {
                return null;
            }

            var model = new EFEntities.Games.Game();
            model.Description = game.Description;
            model.Name = game.Name;
            model.Id = game.Id;
            model.SellingInformation = game.SellingInformation.ToGameSellingInformationEntity();
            model.State = game.ProductState;
            model.TradingInformation = game.TradingInformation.ToGameTradingInformationEntity();
            model.CreatedDate = game.CreatedDate;
            model.ModifiedDate = game.ModifiedDate;

            return model;
        }


        #endregion Product Translations
    }
}
