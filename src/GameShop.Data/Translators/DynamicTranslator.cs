using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Translators
{
    /// <summary>
    /// Translates dynamic data to business objects.
    /// </summary>
    public class DynamicTranslator
    {
        /// <summary>
        /// Translate dynamic object to an Advertisement instance.
        /// </summary>
        /// <param name="dynamicObject">Dynamic object.</param>
        /// <returns>An instance of Advertisement.</returns>
        public static Advertisement TranslateAdvertisement(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            //Initialize advertisement.
            var advertisement = new Advertisement();
            advertisement.AdvertisementId = dynamicObject.AdvertisementId;
            advertisement.FriendlyId = dynamicObject.FriendlyId;
            advertisement.Status = dynamicObject.StatusId;
            advertisement.Title = dynamicObject.Title;
            advertisement.Description = dynamicObject.Description;
            advertisement.ReasonForSelling = dynamicObject.ReasonForSelling;

            return advertisement;
        }

        /// <summary>
        /// Translate dynamic object to a Game instance.
        /// </summary>
        /// <param name="dynamicObject">Dynamic object.</param>
        /// <returns>An instance of Game.</returns>
        public static Game TranslateGame(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var game = new Game();

            //Initialize game information.
            game.ProductId = dynamicObject.GameId;
            game.Title = dynamicObject.Title;
            game.IsForSale = dynamicObject.IsForSale;
            game.IsForTrade = dynamicObject.IsForTrade;
            game.ReasonForSelling = dynamicObject.ReasonForSelling;
            game.GamingPlatform = (GamingPlatform)Enum.Parse(typeof(GamingPlatform), string.Format("{0}", dynamicObject.GamingPlatform));
            game.GameGenre = (GameGenre)Enum.Parse(typeof(GameGenre), string.Format("{0}", dynamicObject.GameGenre));

            //Audit information.
            game.AuditInformation.CreatedDTTM = dynamicObject.CreatedDTTM;
            game.AuditInformation.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            game.AuditInformation.CreatedBy = dynamicObject.CreatedBy;
            game.AuditInformation.ModifiedBy = dynamicObject.ModifiedBy;

            return game;
        }

        public static SellingInformation TranslatePricingInformation(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var pricingInformation = new SellingInformation();
            
            pricingInformation.TradePrice = dynamicObject.TradePrice;
            pricingInformation.SellingPrice = dynamicObject.SalePrice;
            pricingInformation.Currency = dynamicObject.Currency;

            return pricingInformation;
        }

        public static Address TranslateMeetupLocations(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var address = new Address();

            address.Street1 = dynamicObject.StreetOne;
            address.Street2 = dynamicObject.StreetTwo;
            address.Street3 = dynamicObject.StreetThree;
            address.Barangay = dynamicObject.Barangay;
            address.Municipality = dynamicObject.Municipality;
            address.City = dynamicObject.City;
            address.ZipCode = dynamicObject.ZipCode;
            address.Province = dynamicObject.Province;
            address.Region = dynamicObject.Region;
            address.Country = dynamicObject.Country;

            //Audit information.
            address.AuditInformation.CreatedDTTM = dynamicObject.CreatedDTTM;
            address.AuditInformation.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            address.AuditInformation.CreatedBy = dynamicObject.CreatedBy;
            address.AuditInformation.ModifiedBy = dynamicObject.ModifiedBy;

            return address;
        }

        public static AuditInformation TranslateUser(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var auditInformation = new AuditInformation();

            auditInformation.CreatedDTTM = dynamicObject.CreatedDTTM;
            auditInformation.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            auditInformation.CreatedBy = dynamicObject.CreatedBy;
            auditInformation.ModifiedBy = dynamicObject.ModifiedBy;

            return auditInformation;
        }

        public static Address TranslateAddress(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var address = new Address();
            address.AddressId = dynamicObject.AddressId;
            address.Street1 = dynamicObject.Street1;
            address.Street2 = dynamicObject.Street2;
            address.Street3 = dynamicObject.Street3;
            address.Barangay = dynamicObject.Barangay;
            address.Municipality = dynamicObject.Municipality;
            address.City = dynamicObject.City;
            address.ZipCode = dynamicObject.ZipCode;
            address.Province = dynamicObject.Province;
            address.Region = dynamicObject.Region;
            address.Country = dynamicObject.Country;
            /*address.CreatedDTTM = dynamicObject.CreatedDTTM;
            address.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            address.CreatedBy = dynamicObject.CreatedBy;
            address.ModifiedBy = dynamicObject.ModifiedBy;*/

            return address;
        }

        public static ContactInformation TranslateContactInformation(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var contactInformation = new ContactInformation();
            contactInformation.ContactInformationId = dynamicObject.ContactInformationId;
            contactInformation.Email = dynamicObject.Email;
            contactInformation.ContactNumber = dynamicObject.ContactNumber;
            contactInformation.AuditInformation.CreatedDTTM = dynamicObject.CreatedDTTM;
            contactInformation.AuditInformation.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            contactInformation.AuditInformation.CreatedBy = dynamicObject.CreatedBy;
            contactInformation.AuditInformation.ModifiedBy = dynamicObject.ModifiedBy;

            return contactInformation;
        }

        public static Feedback TranslateFeedback(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var feedback = new Feedback();
            feedback.FeedbackId = dynamicObject.FeedbackId;
            feedback.Comments = dynamicObject.Comments;
            feedback.Rating = (Rating)Enum.Parse(typeof(Rating), string.Format("{0}", dynamicObject.Rating));
            feedback.AuditInformation.CreatedDTTM = dynamicObject.CreatedDTTM;
            feedback.AuditInformation.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            feedback.AuditInformation.CreatedBy = dynamicObject.CreatedBy;
            feedback.AuditInformation.ModifiedBy = dynamicObject.ModifiedBy;

            return feedback;
        }
    }
}
