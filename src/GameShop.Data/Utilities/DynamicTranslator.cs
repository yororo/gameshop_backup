﻿using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Utilities
{
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
            advertisement.Title = dynamicObject.Title;
            advertisement.Description = dynamicObject.Description;
            advertisement.Created = dynamicObject.Created;
            advertisement.Modified = dynamicObject.Modified;

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

            //Initialize game.
            var game = new Game();
            game.ProductId = dynamicObject.ProductInformationId;
            game.GameId = dynamicObject.GameId;
            game.Name = dynamicObject.Name;
            game.Description = dynamicObject.Description;
            game.ReleaseDate = dynamicObject.ReleaseDate;
            game.GamingPlatform = (GamingPlatform)Enum.Parse(typeof(GamingPlatform), string.Format("{0}", dynamicObject.GamingPlatform));
            game.GameGenre = (GameGenre)Enum.Parse(typeof(GameGenre), string.Format("{0}", dynamicObject.GameGenre));
            game.CreatedDTTM = dynamicObject.CreatedDTTM;
            game.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            game.CreatedBy = dynamicObject.CreatedBy;
            game.ModifiedBy = dynamicObject.ModifiedBy;

            return game;
        }

        public static PricingInformation TranslatePricingInformation(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var pricingInformation = new PricingInformation();
            pricingInformation.PricingInformationId = dynamicObject.PricingInformationId;
            pricingInformation.Price = dynamicObject.Price;
            pricingInformation.Currency = (Currency)Enum.Parse(typeof(Currency), string.Format("{0}", dynamicObject.Currency));
            pricingInformation.Created = dynamicObject.Created;
            pricingInformation.Modified = dynamicObject.Modified;

            return pricingInformation;
        }

        public static User TranslateUser(dynamic dynamicObject)
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            var user = new User();
            user.UserId = dynamicObject.UserId;
            user.Name.Salutation = (Salutation)Enum.Parse(typeof(Salutation), dynamicObject.Salutation);
            user.Name.FirstName = dynamicObject.FirstName;
            user.Name.MiddleName = dynamicObject.MiddleName;
            user.Name.LastName = dynamicObject.LastName;
            user.Name.Suffix = dynamicObject.Suffix;
            user.CreatedDTTM = dynamicObject.CreatedDTTM;
            user.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            user.CreatedBy = dynamicObject.CreatedBy;
            user.ModifiedBy = dynamicObject.ModifiedBy;

            return user;
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
            address.CreatedDTTM = dynamicObject.CreatedDTTM;
            address.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            address.CreatedBy = dynamicObject.CreatedBy;
            address.ModifiedBy = dynamicObject.ModifiedBy;

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
            contactInformation.CreatedDTTM = dynamicObject.CreatedDTTM;
            contactInformation.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            contactInformation.CreatedBy = dynamicObject.CreatedBy;
            contactInformation.ModifiedBy = dynamicObject.ModifiedBy;

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
            feedback.CreatedDTTM = dynamicObject.CreatedDTTM;
            feedback.ModifiedDTTM = dynamicObject.ModifiedDTTM;
            feedback.CreatedBy = dynamicObject.CreatedBy;
            feedback.ModifiedBy = dynamicObject.ModifiedBy;

            return feedback;
        }
    }
}
