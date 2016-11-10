using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using System.Data;
using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;
using GameShop.Contracts.Enumerations;
using Dapper;
using System.Data.Common;
using System.Text;
using System.Diagnostics;
using GameShop.Data.Extensions;
using GameShop.Data.Translators;

namespace GameShop.Data.Repositories
{
    public class GameAdvertisementRepository : Repository, IGameAdvertisementRepository
    {
        public GameAdvertisementRepository(IDatabaseProviderClient databaseProviderClient) 
            : base(databaseProviderClient)
        {

        }

        #region IAdvertisementAsyncRepository Implementation

        public async Task<Advertisement> FindByIdAsync(Guid id)
        {
            string findByIdQuery = @"SELECT * FROM Advertisements ad
                                    INNER JOIN Users owner
                                    ON ad.OwnerId = owner.UserId
                                    WHERE ad.AdvertisementId = @AdvertisementId;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var command = new CommandDefinition(findByIdQuery, new { AdvertisementId = id });
                //return await databaseConnection.QueryAsync<Game>("spGetAll", new { GamingPlatform }, commandType: CommandType.StoredProcedure);

                //Load advertisement data.
                var advertisementData = await databaseConnection.QuerySingleOrDefaultAsync(command).ConfigureAwait(false);

                //No result found.
                if(advertisementData == null)
                {
                    return null;
                }

                //Instantiate.
                var advertisement = DynamicTranslator.TranslateAdvertisement(advertisementData);

                //Load advertisement products.
                var products = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.Products.AddRange(products);

                //Load advertisement owner. Full.
                advertisement.Owner = await GetAdOwnerAsync(advertisement.AdvertisementId).ConfigureAwait(false);

                //Load meetup locations.
                var meetupLocations = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.MeetupLocations.AddRange(meetupLocations);

                return advertisement;
            }
        }

        public async Task<Advertisement> FindByFriendlyIdAsync(string id)
        {
            string findByFriendlyIdQuery = @"SELECT * FROM Advertisements ad
                                            INNER JOIN Users owner
                                            ON ad.OwnerId = owner.UserId
                                            WHERE ad.FriendlyId = @FriendlyId;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var command = new CommandDefinition(findByFriendlyIdQuery, new { FriendlyId = id });

                //Load advertisement data.
                var advertisementData = await databaseConnection.QuerySingleOrDefaultAsync(command).ConfigureAwait(false);
                
                //No result found.
                if (advertisementData == null)
                {
                    return null;
                }

                //Instantiate.
                var advertisement = DynamicTranslator.TranslateAdvertisement(advertisementData);
                
                //Load advertisement products.
                var products = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.Products.AddRange(products);

                //Load advertisement owner. Full.
                advertisement.Owner = await GetAdOwnerAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                
                //Load meetup locations.
                var meetupLocations = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.MeetupLocations.AddRange(meetupLocations);

                return advertisement;
            }
        }

        public async Task<IEnumerable<Advertisement>> FindByTitleAsync(string title)
        {
            string findByTitleQuery = @"SELECT * FROM Advertisements ad
                                        INNER JOIN Users owner
                                        ON ad.OwnerId = owner.UserId
                                        WHERE ad.Title LIKE @Title;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var advertisements = new List<Advertisement>();

                var advertisementsData = await databaseConnection.QueryAsync(findByTitleQuery, new { Title = string.Format("%{0}%", title) }).ConfigureAwait(false);
                foreach (var advertisementData in advertisementsData)
                {
                    //Instantiate.
                    var advertisement = DynamicTranslator.TranslateAdvertisement(advertisementData);
                    //Load advertisement owner. Partial.
                    advertisement.Owner = DynamicTranslator.TranslateUser(advertisementData);

                    //Load advertisement products.
                    var games = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                    advertisement.Products.AddRange(games);

                    //Load meetup locations.
                    var meetupLocations = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                    advertisement.MeetupLocations.AddRange(meetupLocations);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        /// <summary>
        /// Loads advertisements with products and basic owner information.
        /// </summary>
        /// <returns>IEnumerable list of Advertisements.</returns>
        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            string spGetAllAsync = @"spGetAllAsync";

            using (var databaseConnection = Client.CreateConnection())
            {
                var advertisements = new List<Advertisement>();
                
                var advertisementsData = await databaseConnection.QueryAsync(spGetAllAsync, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                foreach (var advertisementData in advertisementsData)
                {
                    //Load advertisements.
                    Advertisement advertisement = DynamicTranslator.TranslateAdvertisement(advertisementData);

                    //Load advertisement audit information such as CreatedBy, CreatedDTTM, ModifiedBy, and ModifiedDTTM.
                    advertisement.AuditInformation = DynamicTranslator.TranslateUser(advertisementData);

                    //Load all games in the advertisement.
                    var games = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                    advertisement.Products.AddRange(games);

                    //Load meetup locations.
                    var meetupLocationsData = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);

                    advertisement.MeetupLocations.AddRange(meetupLocationsData);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        public async Task<IEnumerable<Advertisement>> GetAllDeepAsync()
        {
            string getAllAdsMultipleQuery = @"SELECT * 
                                FROM Advertisements ad
                                INNER JOIN Users owner
                                ON ad.OwnerId = owner.UserId;

                                SELECT *
                                FROM Games game 
                                INNER JOIN ProductInformation product
                                ON game.ProductInformationId = product.ProductInformationId
                                INNER JOIN PricingInformation pricing
                                ON product.PricingInformationId = pricing.PricingInformationId;

                                SELECT * FROM Addresses;
                                SELECT * FROM AdvertisementsAddresses;
                                SELECT * FROM UsersAddresses;

                                SELECT * FROM ContactInformation;
                                SELECT * FROM UsersContactInformation;";

            using (var databaseConnection = Client.CreateConnection())
            {
                //Initialize.
                var advertisements = new List<Advertisement>();

                var mapper = await databaseConnection.QueryMultipleAsync(getAllAdsMultipleQuery).ConfigureAwait(false);

                //Read results of first query.
                //Load advertisement data from query.
                var advertisementsData = await mapper.ReadAsync().ConfigureAwait(false);

                //No results found.
                if (advertisementsData == null)
                {
                    return advertisements;
                }

                //Read results of second query.
                //Load game data from query.
                var productsData = await mapper.ReadAsync().ConfigureAwait(false);

                //Map advertisements to an instance.
                foreach (var advertisementData in advertisementsData)
                {
                    //Translate advertisement.
                    Advertisement advertisement = DynamicTranslator.TranslateAdvertisement(advertisementData);

                    //Initialize.
                    var games = new List<Game>();

                    foreach (var productData in productsData)
                    {
                        if (productData.AdvertisementId == advertisement.AdvertisementId)
                        {
                            //Translate game.
                            Game game = DynamicTranslator.TranslateGame(productData);

                            //Instantiate pricing information.
                            game.SellingInformation = DynamicTranslator.TranslatePricingInformation(productData);
                            
                            //Add game to the advertisement.
                            advertisement.Products.Add(game);
                        }
                    }

                    //Read results of third query.
                    //Load all addresses.
                    var addresses = await mapper.ReadAsync<Address>().ConfigureAwait(false);

                    //Read results of fourth query.
                    //Load advertisement meetup locations.
                    var advertisementsAddressesMap = await mapper.ReadAsync().ConfigureAwait(false);

                    //Load all meetup locations.
                    foreach(var map in advertisementsAddressesMap)
                    {
                        if(map.AdvertisementId == advertisementData.AdvertisementId)
                        {
                            var address = addresses.FirstOrDefault(a => map.AddressId == a.AddressId);

                            //Add address to meetup locations.
                            advertisement.MeetupLocations.Add(address);
                        }
                    }

                    //Translate user ad owner.
                    //advertisement.Owner = DynamicTranslator.TranslateUser(advertisementData);

                    //Read results of sixth query.
                    //Load user and address mapping.
                    var usersAddressesMap = await mapper.ReadAsync().ConfigureAwait(false);

                    //Load all user addresses.
                    foreach (var map in usersAddressesMap)
                    {
                        /*if (map.UserId == advertisement.Owner.UserId)
                        {
                            var result = addresses.FirstOrDefault(address => map.AddressId == address.AddressId);
                            if (result != null)
                            {
                                advertisement.Owner.Addresses.Add(result);
                            }
                        }*/
                    }

                    //Read results of fifth query.
                    //Load all contact information.
                    var contactsData = await mapper.ReadAsync<ContactInformation>().ConfigureAwait(false);

                    //Read results of seventh query.
                    //Load user and contact information mapping.
                    var usersContactsMap = await mapper.ReadAsync().ConfigureAwait(false);

                    //Load all user contact information.
                    foreach (var map in usersContactsMap)
                    {
                        /*if (map.UserId == advertisement.Owner.UserId)
                        {
                            var result = contactsData.FirstOrDefault(contact => map.ContactInformationId == contact.ContactInformationId);
                            if (result != null)
                            {
                                advertisement.Owner.ContactInformation.Add(result);
                            }
                        }*/
                    }

                    //Add to list.
                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        public async Task<IEnumerable<Address>> GetMeetupLocationsAsync(Guid id)
        {
            string getMeetupLocationsQuery = @"GetMeetupLocationsAsync";

            using (var databaseConnection = Client.CreateConnection())
            {
                var meetupLocations = new List<Address>();

                var tempMeetupLocations = await databaseConnection.QueryAsync<Address>(getMeetupLocationsQuery, new { AdvertisementId = id }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                foreach (var tempMeetupLocation in tempMeetupLocations)
                {
                    Address tempAddress = new Address();

                    tempAddress = DynamicTranslator.TranslateMeetupLocations(tempMeetupLocation);

                    meetupLocations.Add(tempAddress);
                }

                return meetupLocations;
            }
        }

        public async Task<IEnumerable<Game>> GetProductsAsync(Guid id)
        {
            string getAdGamesQuery = @"spGetProductsAsync";

            using (var databaseConnection = Client.CreateConnection())
            {
                var games = new List<Game>();

                //Load games data.

                var gamesData = await databaseConnection.QueryAsync(getAdGamesQuery, new { AdvertisementId = id }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                foreach (var gameData in gamesData)
                {
                    Game game = DynamicTranslator.TranslateGame(gameData);

                    game.SellingInformation = DynamicTranslator.TranslatePricingInformation(gameData);

                    games.Add(game);
                }

                return games;
            }
        }

        public async Task<User> GetAdOwnerAsync(Guid id)
        {
            string spGetAdOwnerAsync = @"spGetAdOwnerAsync";

            string spGetUserProfileInfo = @"spGetUserProfileInfo";

            using (var databaseConnection = Client.CreateConnection())
            {
                var command = new CommandDefinition(spGetAdOwnerAsync, new { AdvertisementId = id }, commandType: CommandType.StoredProcedure);

                //Load user.
                var userData = await databaseConnection.QuerySingleOrDefaultAsync(command).ConfigureAwait(false);

                //No results found.
                if(userData == null)
                {
                    return null;
                }

                var user = DynamicTranslator.TranslateUser(userData);

                var profileInfos = await databaseConnection.QueryAsync(spGetUserProfileInfo, new { UserId = user.UserId }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                foreach(var profileData in profileInfos)
                {
                    user.Addresses.AddRange(profileData);
                }

                /*var mapper = await databaseConnection.QueryMultipleAsync(getUserAddressesAndContactsQuery, new { UserId = user.UserId }).ConfigureAwait(false);

                //Load user addresses.
                var userAddresses = await mapper.ReadAsync<Address>().ConfigureAwait(false);
                user.Addresses.AddRange(userAddresses);

                //Load user contact information.
                var userContacts = await mapper.ReadAsync<ContactInformation>().ConfigureAwait(false);
                user.ContactInformation.AddRange(userContacts);*/

                /*//Load user feedbacks.
                var userFeedbacks = await mapper.ReadAsync().ConfigureAwait(false);
                foreach(var userFeedback in userFeedbacks)
                {
                    Feedback feedback = DynamicTranslator.TranslateFeedback(userFeedback);
                    feedback.User = user;
                    //feedback.Owner = new User();

                    user.Feedbacks.Add(feedback);
                }*/

                return user;
            }
        }

        public async Task<int> AddAsync(Advertisement advertisement)
        {
            using (var databaseConnection = Client.CreateConnection())
            {
                return await databaseConnection.ExecuteAsync(
                    $"INSERT INTO Advertisements(Id, FriendlyId, Title, Description, OwnerID, Created, Modified) VALUES(@Id, @FriendlyId, @Title, @Description, @OwnerID, @Created, @Modified)",
                    new
                    {
                        Id = Guid.Empty,
                        FriendlyId = advertisement.FriendlyId,
                        Title = advertisement.Title,
                        Description = advertisement.Description,
                        //OwnerID = advertisement.Owner.UserId,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    }
                ).ConfigureAwait(false);
            }
        }

        public async Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteByIdAsync(Guid id)
        {
            using (var databaseConnection = Client.CreateConnection())
            {
                return await databaseConnection.ExecuteAsync($"DELETE FROM Advertisements WHERE Id LIKE @Id", new { Id = id }).ConfigureAwait(false);
            }
        }

        Task<Advertisement<Game>> IAdvertisementRepository<Guid, Game>.FindByIdAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        Task<Advertisement<Game>> IAdvertisementRepository<Guid, Game>.FindByFriendlyIdAsync(string friendlyId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.FindByTitleAsync(string advertisementTitle)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.GetAllDeepAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid advertisementId, Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
