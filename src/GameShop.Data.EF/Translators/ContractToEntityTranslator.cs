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
        public static EFEntities.User ToUserEntity(this User user)
        {
            // Guard clause.
            if(user == null)
            {
                return null;
            }

            var model = new EFEntities.User();

            model.UserId = user.UserId;
            model.Account = user.Account.ToAccountEntity(model);
            model.Profile = user.Profile.ToProfileEntity(model);
            model.CreatedDate = user.CreatedDate;
            model.ModifiedDate = user.ModifiedDate;

            return model;
        }

        public static EFEntities.Account ToAccountEntity(this Account account, EFEntities.User parentUser = null)
        {
            // Guard clause.
            if (account == null)
            {
                return null;
            }

            var model = new EFEntities.Account();

            model.AccountId = account.AccountId;
            model.Username = account.Username;
            model.Email = account.Email;
            model.EmailVerified = account.EmailVerified;
            model.PasswordHash = account.PasswordHash;
            model.IsActive = account.IsActive;
            model.CreatedDate = account.CreatedDate;
            model.ModifiedDate = account.ModifiedDate;

            if (parentUser != null)
            {
                model.UserId = parentUser.UserId;
                model.User = parentUser;
            }

            return model;
        }

        public static EFEntities.Profile ToProfileEntity(this Profile profile, EFEntities.User parentUser = null)
        {
            // Guard clause.
            if (profile == null)
            {
                return null;
            }

            var model = new EFEntities.Profile();

            model.ProfileId = profile.ProfileId;
            model.Salutation = profile.Name.Salutation;
            model.FirstName = profile.Name.FirstName;
            model.MiddleName = profile.Name.MiddleName;
            model.LastName = profile.Name.LastName;
            model.Suffix = profile.Name.Suffix;
            model.Gender = profile.Gender;
            model.CivilStatus = profile.CivilStatus;
            model.Birthday = profile.Birthday;
            model.CreatedDate = profile.CreatedDate;
            model.ModifiedDate = profile.ModifiedDate;
            model.Addresses = new List<EFEntities.ProfileAddress>();
            model.ContactInformation = new List<EFEntities.ProfileContactInformation>();

            foreach(Address address in profile.Addresses)
            {
                model.Addresses.Add(address.ToProfileAddressEntity(model));
            }

            foreach(ContactInformation contactInformation in profile.ContactInformation)
            {
                model.ContactInformation.Add(contactInformation.ToProfileContactInformationEntity(model));
            }

            if(parentUser != null)
            {
                model.UserId = parentUser.UserId;
                model.User = parentUser;
            }

            return model;
        }

        public static EFEntities.ProfileAddress ToProfileAddressEntity(this Address address, EFEntities.Profile parentProfile = null)
        {
            // Guard clause.
            if (address == null)
            {
                return null;
            }

            var model = new EFEntities.ProfileAddress();

            model.AddressId = address.AddressId;
            model.Street1 = address.Street1;
            model.Street2 = address.Street2;
            model.Street3 = address.Street3;
            model.Barangay = address.Barangay;
            model.Municipality = address.Municipality;
            model.City = address.City;
            model.ZipCode = address.ZipCode;
            model.Province = address.Province;
            model.Region = address.Region;
            model.Country = address.Country;
            model.CreatedDate = address.CreatedDate;
            model.ModifiedDate = address.ModifiedDate;

            if (parentProfile != null)
            {
                model.ProfileId = parentProfile.ProfileId;
                model.Profile = parentProfile;
            }

            return model;
        }

        public static EFEntities.ProfileContactInformation ToProfileContactInformationEntity(this ContactInformation contactInformation,
                                                                                                EFEntities.Profile parentProfile = null)
        {
            // Guard clause.
            if (contactInformation == null)
            {
                return null;
            }

            var model = new EFEntities.ProfileContactInformation();

            model.ContactInformationId = contactInformation.ContactInformationId;
            model.Email = contactInformation.Email;
            model.MobileNumber = contactInformation.MobileNumber;
            model.CreatedDate = contactInformation.CreatedDate;
            model.ModifiedDate = contactInformation.ModifiedDate;

            if (parentProfile != null)
            {
                model.ProfileId = parentProfile.ProfileId;
                model.Profile = parentProfile;
            }

            return model;
        }

        public static EFEntities.Advertisement ToAdvertisementEntity(this Advertisement advertisement)
        {
            // Guard clause.
            if (advertisement == null)
            {
                return null;
            }

            var model = new EFEntities.Advertisement();

            model.AdvertisementId = advertisement.AdvertisementId;
            model.CreatedDate = advertisement.CreatedDate;
            model.Description = advertisement.Description;
            model.FriendlyId = advertisement.FriendlyId;
            model.ModifiedDate = advertisement.ModifiedDate;
            model.State = advertisement.State;
            model.Title = advertisement.Title;

            return model;
        }

        public static EFEntities.SellingInformation ToSellingInformationEntity(this SellingInformation sellingInformation)
        {
            // Guard clause.
            if (sellingInformation == null)
            {
                return null;
            }

            var model = new EFEntities.SellingInformation();

            model.Currency = sellingInformation.Currency;
            model.ReasonForSelling = sellingInformation.ReasonForSelling;
            model.SellingInformationId = sellingInformation.SellingInformationId;
            model.SellingPrice = sellingInformation.SellingPrice;
            model.CreatedDate = sellingInformation.CreatedDate;
            model.ModifiedDate = sellingInformation.ModifiedDate;

            return model;
        }

        public static EFEntities.TradingInformation ToTradingInformationEntity(this TradingInformation tradingInformation)
        {
            // Guard clause.
            if (tradingInformation == null)
            {
                return null;
            }

            var model = new EFEntities.TradingInformation();

            model.CashAmountToAdd = tradingInformation.CashAmountToAdd;
            model.Currency = tradingInformation.Currency;
            model.IsOwnerWillingToAddCash = tradingInformation.IsOwnerWillingToAddCash;
            model.IsOwnerWillingToReceiveCash = tradingInformation.IsOwnerWillingToReceiveCash;
            model.ReasonForSelling = tradingInformation.ReasonForTrading;
            model.TradeNotes = tradingInformation.TradeNotes;
            model.TradingInformationId = tradingInformation.TradingInformationId;
            model.TradingPrice = tradingInformation.TradingPrice;
            model.CreatedDate = tradingInformation.CreatedDate;
            model.ModifiedDate = tradingInformation.ModifiedDate;

            return model;
        }

        #region Product Translations

        public static EFEntities.Game ToGameEntity(this Game game)
        {
            // Guard clause.
            if (game == null)
            {
                return null;
            }

            var model = new EFEntities.Game();
            model.Advertisement = game.Advertisement.ToAdvertisementEntity();
            model.Description = game.Description;
            model.Name = game.Name;
            model.ProductId = game.ProductId;
            model.SellingInformation = game.SellingInformation.ToSellingInformationEntity();
            model.State = game.ProductState;
            model.TradingInformation = game.TradingInformation.ToTradingInformationEntity();
            model.CreatedDate = game.CreatedDate;
            model.ModifiedDate = game.ModifiedDate;

            return model;
        }


        #endregion Product Translations
    }
}
