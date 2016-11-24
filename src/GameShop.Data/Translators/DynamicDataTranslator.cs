using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Translators
{
    /// <summary>
    /// Translates dynamic database data objects to business objects.
    /// </summary>
    /// <remarks>
    /// The dynamic data passed in as parameter exposes the actual database columns as properties.
    /// </remarks>
    public class DynamicDataTranslator
    {
        /// <summary>
        /// Translate dynamic object from database to an Advertisement instance.
        /// </summary>
        /// <param name="advertisementData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Advertisement.</returns>
        public static Advertisement TranslateAdvertisement(dynamic advertisementData)
        {
            if (advertisementData == null)
            {
                return null;
            }

            //Initialize advertisement.
            var advertisement = new Advertisement();
            advertisement.AdvertisementId = advertisementData.AdvertisementId;
            advertisement.FriendlyId = advertisementData.FriendlyId;
            advertisement.Title = advertisementData.Title;
            advertisement.Description = advertisementData.Description;
            advertisement.State = parseEnum<AdvertisementState>(advertisementData.State);
            advertisement.CreatedDate = advertisementData.CreatedDate;
            advertisement.ModifiedDate = advertisementData.ModifiedDate;

            return advertisement;
        }

        /// <summary>
        /// Translate dynamic object from database to a Game instance.
        /// </summary>
        /// <param name="gameData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Game.</returns>
        public static Game TranslateGame(dynamic gameData)
        {
            if (gameData == null)
            {
                return null;
            }

            var game = new Game();

            //Initialize game information.
            game.ProductId = gameData.GameId;
            game.Name = gameData.Name;
            game.Description = gameData.Description;
            game.ProductState = parseEnum<ProductState>($"{ gameData.State }");
            game.GamingPlatform = parseEnum<GamePlatform>($"{ gameData.GamingPlatform }");
            game.GameGenre = parseEnum<GameGenre>($"{ gameData.GameGenre }");
            game.SellingInformation = TranslateSellingInformation(gameData);
            game.TradingInformation = TranslateSellingInformation(gameData);
            game.CreatedDate = gameData.CreatedDate;
            game.ModifiedDate = gameData.ModifiedDate;

            return game;
        }

        /// <summary>
        /// Translate dynamic object from database to a SellingInformation instance.
        /// </summary>
        /// <param name="sellingInformationData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of SellingInformation.</returns>
        public static SellingInformation TranslateSellingInformation(dynamic sellingInformationData)
        {
            if (sellingInformationData == null)
            {
                return null;
            }

            var sellingInformation = new SellingInformation();
            
            sellingInformation.SellingInformationId = sellingInformationData.SellingInformationId;
            sellingInformation.Currency = parseEnum<Currency>($"{ sellingInformationData.Currency }");
            sellingInformation.SellingPrice = sellingInformationData.SalePrice;
            sellingInformation.ReasonForSelling = sellingInformationData.ReasonForSelling;
            sellingInformation.CreatedDate = sellingInformationData.CreatedDate;
            sellingInformation.ModifiedDate = sellingInformationData.ModifiedDate;

            return sellingInformation;
        }

        /// <summary>
        /// Translate dynamic object from database to a TradingInformation instance.
        /// </summary>
        /// <param name="tradingInformationData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of TradingInformation.</returns>
        public static TradingInformation TranslateTradingInformation(dynamic tradingInformationData)
        {
            if (tradingInformationData == null)
            {
                return null;
            }

            var tradingInformation = new TradingInformation();

            tradingInformation.TradingInformationId = tradingInformationData.TradingInformationId;
            tradingInformation.Currency = parseEnum<Currency>($"{ tradingInformationData.Currency }");
            tradingInformation.TradingPrice = tradingInformationData.TradingPrice;
            tradingInformation.ReasonForTrading = tradingInformationData.ReasonForTrading;
            tradingInformation.IsOwnerWillingToAddCash = tradingInformationData.IsOwnerWillingToAddCash;
            tradingInformation.CashAmountToAdd = tradingInformationData.CashAmountToAdd;
            tradingInformation.IsOwnerWillingToReceiveCash = tradingInformationData.IsOwnerWillingToReceiveCash;
            tradingInformation.TradeNotes = tradingInformationData.TradeNotes;
            tradingInformation.CreatedDate = tradingInformationData.CreatedDate;
            tradingInformation.ModifiedDate = tradingInformationData.ModifiedDate;

            return tradingInformation;
        }

        /// <summary>
        /// Translate dynamic object from database to a User instance.
        /// </summary>
        /// <param name="userData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of User.</returns>
        public static User TranslateUser(dynamic userData)
        {
            if (userData == null)
            {
                return null;
            }

            var user = new User();

            user.UserId = userData.UserId;
            user.Account = TranslateAccount(userData);
            user.Profile = TranslateProfile(userData);
            user.CreatedDate = userData.CreatedDate;
            user.ModifiedDate = userData.ModifiedDate;

            return user;
        }

        /// <summary>
        /// Translate dynamic object from database to a Profile instance.
        /// </summary>
        /// <param name="profileData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Profile.</returns>
        public static Profile TranslateProfile(dynamic profileData)
        {
            if (profileData == null)
            {
                return null;
            }

            var profile = new Profile();

            profile.ProfileId = profileData.ProfileId;
            profile.Name = TranslateName(profileData);
            profile.Gender = parseEnum<Gender>($"{ profileData.Gender }");
            profile.Birthday = profileData.Birthday;
            profile.CivilStatus = parseEnum<CivilStatus>($"{ profileData.CivilStatus }");
            profile.CreatedDate = profileData.CreatedDate;
            profile.ModifiedDate = profileData.ModifiedDate;

            return profile;
        }

        /// <summary>
        /// Translate dynamic object from database to a Name instance.
        /// </summary>
        /// <param name="nameData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Name.</returns>
        public static Name TranslateName(dynamic nameData)
        {
            if (nameData == null)
            {
                return null;
            }

            var name = new Name();
            name.Salutation = parseEnum<Salutation>($"{ nameData.Salutation }");
            name.FirstName = nameData.FirstName;
            name.MiddleName = nameData.MiddleName;
            name.LastName = nameData.LastName;
            name.Suffix = nameData.Suffix;

            return name;
        }

        /// <summary>
        /// Translate dynamic object from database to an Account instance.
        /// </summary>
        /// <param name="accountData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Account.</returns>
        public static Account TranslateAccount(dynamic accountData)
        {
            if (accountData == null)
            {
                return null;
            }

            var account = new Account();

            account.AccountId = accountData.AccountId;
            account.Email = accountData.Email;
            account.EmailVerified = accountData.EmailConfirmed;
            account.Username = accountData.Username;
            account.PasswordHash = accountData.PasswordHash;
            account.IsActive = accountData.IsActive;
            account.CreatedDate = accountData.CreatedDate;
            account.ModifiedDate = accountData.ModifiedDate;

            return account;
        }

        /// <summary>
        /// Translate dynamic object from database to an Address instance.
        /// </summary>
        /// <param name="addressData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Address.</returns>
        public static Address TranslateAddress(dynamic addressData)
        {
            if (addressData == null)
            {
                return null;
            }

            var address = new Address();
            address.AddressId = addressData.AddressId;
            address.Street1 = addressData.Street1;
            address.Street2 = addressData.Street2;
            address.Street3 = addressData.Street3;
            address.Barangay = addressData.Barangay;
            address.Municipality = addressData.Municipality;
            address.City = addressData.City;
            address.ZipCode = addressData.ZipCode;
            address.Province = addressData.Province;
            address.Region = addressData.Region;
            address.Country = addressData.Country;
            address.CreatedDate = addressData.CreatedDate;
            address.ModifiedDate = addressData.ModifiedDate;

            return address;
        }

        /// <summary>
        /// Translate dynamic object from database to a ContactInformation instance.
        /// </summary>
        /// <param name="contactInformationData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of ContactInformation.</returns>
        public static ContactInformation TranslateContactInformation(dynamic contactInformationData)
        {
            if (contactInformationData == null)
            {
                return null;
            }

            var contactInformation = new ContactInformation();
            contactInformation.ContactInformationId = contactInformationData.ContactInformationId;
            contactInformation.Email = contactInformationData.Email;
            contactInformation.MobileNumber = contactInformationData.ContactNumber;
            contactInformation.CreatedDate = contactInformationData.CreatedDate;
            contactInformation.ModifiedDate = contactInformationData.ModifiedDate;

            return contactInformation;
        }

        /// <summary>
        /// Translate dynamic object from database to a Feedback instance.
        /// </summary>
        /// <param name="feedbackData">Dynamic object which exposes the table columns as properties.</param>
        /// <returns>An instance of Feedback.</returns>
        public static Feedback TranslateFeedback(dynamic feedbackData)
        {
            if (feedbackData == null)
            {
                return null;
            }

            var feedback = new Feedback();
            feedback.FeedbackId = feedbackData.FeedbackId;
            feedback.Review = feedbackData.Comments;
            feedback.Rating = parseEnum<Rating>(feedbackData.Rating);
            feedback.CreatedDate = feedbackData.CreatedData;
            feedback.ModifiedDate = feedbackData.ModifiedDate;

            return feedback;
        }

        #region Functions

        /// <summary>
        /// Parse string to enums.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum to convert to.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns></returns>
        private static TEnum parseEnum<TEnum>(string value)
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            catch(Exception ex)
            {
                throw new Exception($"Unable to parse value: { value } to type { typeof(TEnum).Name }", ex);
            }
        }

        #endregion Functions
    }
}
