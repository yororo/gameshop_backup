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
        public static User ToUserContract(this Entities.User model)
        {
            // Guard clause.
            if (model == null)
            {
                return null;
            }

            var user = new User();

            user.UserId = model.Id;
            user.Account = new Account()
            {
                Username = model.UserName,
                Email = model.Email,
                //EmailConfirmed = model.EmailConfirmed,
                PhoneNumber = model.PhoneNumber,
                //PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                //CreatedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue),
                //ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue)
            };

            user.Profile = model.Profile.ToProfileContract();
            user.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
            user.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

            return user;
        }

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

        public static Profile ToProfileContract(this EFEntities.Profile model)
        {
            // Guard clause.
            if (model == null)
            {
                return null;
            }

            var profile = new Profile();

            profile.ProfileId = model.Id;
            profile.Name.Salutation = model.Salutation;
            profile.Name.FirstName = model.FirstName;
            profile.Name.MiddleName = model.MiddleName;
            profile.Name.LastName = model.LastName;
            profile.Name.Suffix = model.Suffix;
            profile.Gender = model.Gender;
            profile.CivilStatus = model.CivilStatus;
            profile.Birthday = model.Birthday;
            profile.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
            profile.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

            foreach (EFEntities.Address address in model.Addresses)
            {
                profile.Addresses.Add(address.ToProfileAddressContract());
            }

            foreach (EFEntities.ContactInformation contactInformation in model.ContactInformation)
            {
                profile.ContactInformation.Add(contactInformation.ToProfileContactInformationContract());
            }

            return profile;
        }

        public static Address ToProfileAddressContract(this EFEntities.Address model)
        {
            // Guard clause.
            if (model == null)
            {
                return null;
            }

            var address = new Address();

            //address.AddressId = model.Id;
            address.Street1 = model.Street1;
            address.Street2 = model.Street2;
            address.Street3 = model.Street3;
            address.Barangay = model.Barangay;
            address.Municipality = model.Municipality;
            address.City = model.City;
            address.ZipCode = model.ZipCode;
            address.Province = model.Province;
            address.Region = model.Region;
            address.Country = model.Country;
            //address.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
            //address.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

            return address;
        }

        public static ContactInformation ToProfileContactInformationContract(this EFEntities.ContactInformation model)
        {
            // Guard clause.
            if (model == null)
            {
                return null;
            }

            var contactInformation = new ContactInformation();

           // contactInformation.ContactInformationId = model.Id;
            contactInformation.Email = model.Email;
            contactInformation.PhoneNumber = model.MobileNumber;
            //contactInformation.CreatedDate = model.CreatedDate.GetValueOrDefault(DateTime.MaxValue);
            //contactInformation.ModifiedDate = model.ModifiedDate.GetValueOrDefault(DateTime.MaxValue);

            return contactInformation;
        }
    }
}
