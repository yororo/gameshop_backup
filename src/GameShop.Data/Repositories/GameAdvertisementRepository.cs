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
using GameShop.Data.Utilities;

namespace GameShop.Data.Repositories
{
    public class GameAdvertisementRepository : Repository, IGameAdvertisementAsyncRepository
    {
        private List<Advertisement> _ads;
        private List<PCGame> _pcGames;
        private List<ConsoleGame> _consoleGames;

        public GameAdvertisementRepository(IDatabaseProviderClient databaseProviderClient) 
            : base(databaseProviderClient)
        {
            _ads = new List<Advertisement>();

            _pcGames = new List<PCGame>()
            {
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Test 1", GameGenre = GameGenre.Action },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Dragon Age 3: Inquisition", GameGenre = GameGenre.RPG },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Witcher 3", GameGenre = GameGenre.RPG },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Mass Effect 4", GameGenre = GameGenre.SciFi },
                new PCGame() { ProductId = Guid.NewGuid(), Name = "Torchlight", GameGenre = GameGenre.Simulation, SystemRequirements = new ComputerSpecification() { CPU = new CPU() { Cores = 4, Name = "Intel i7 5700", ClockSpeed = "3.40" } } }
            };


            _consoleGames = new List<ConsoleGame>()
            {
                new ConsoleGame() { ProductId = Guid.NewGuid(), Name = "PS2 Game", GameGenre = GameGenre.RPG, GamingPlatform = GamingPlatform.Xbox360 },
                new ConsoleGame() { ProductId = Guid.NewGuid(), Name = "3DS Game", GameGenre = GameGenre.Simulation, GamingPlatform = GamingPlatform.PlayStation2 }
            };


            var pcGamesAd = new Advertisement(_pcGames);
            pcGamesAd.AdvertisementId = Guid.NewGuid();
            pcGamesAd.FriendlyId = "123";
            pcGamesAd.Title = "PC Games For Sale!";
            pcGamesAd.Description = "Test Description For PC Games Ad.";

            var consoleGamesAd = new Advertisement(_consoleGames);
            consoleGamesAd.AdvertisementId = Guid.NewGuid();
            consoleGamesAd.FriendlyId = "321";
            consoleGamesAd.Title = "Console Games For Sale!";
            consoleGamesAd.Description = "Test Description For Console Games Ad.";

            _ads.Add(pcGamesAd);
            _ads.Add(consoleGamesAd);

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
            string getAdsWithOwnerQuery = @"SELECT * 
                                            FROM Advertisements ad
                                            INNER JOIN Users owner
                                            ON ad.OwnerId = owner.UserId;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var advertisements = new List<Advertisement>();

                //Load advertisements.
                var advertisementsData = await databaseConnection.QueryAsync(getAdsWithOwnerQuery).ConfigureAwait(false);
                foreach (var advertisementData in advertisementsData)
                {
                    Advertisement advertisement = DynamicTranslator.TranslateAdvertisement(advertisementData);
                    //Load advertisement owner. Partial.
                    advertisement.Owner = DynamicTranslator.TranslateUser(advertisementData);

                    //Load games.
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
                            game.PricingInformation = DynamicTranslator.TranslatePricingInformation(productData);
                            
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
                    advertisement.Owner = DynamicTranslator.TranslateUser(advertisementData);

                    //Read results of sixth query.
                    //Load user and address mapping.
                    var usersAddressesMap = await mapper.ReadAsync().ConfigureAwait(false);

                    //Load all user addresses.
                    foreach (var map in usersAddressesMap)
                    {
                        if (map.UserId == advertisement.Owner.UserId)
                        {
                            var result = addresses.FirstOrDefault(address => map.AddressId == address.AddressId);
                            if (result != null)
                            {
                                advertisement.Owner.Addresses.Add(result);
                            }
                        }
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
                        if (map.UserId == advertisement.Owner.UserId)
                        {
                            var result = contactsData.FirstOrDefault(contact => map.ContactInformationId == contact.ContactInformationId);
                            if (result != null)
                            {
                                advertisement.Owner.ContactInformation.Add(result);
                            }
                        }
                    }

                    //Add to list.
                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        public async Task<IEnumerable<Address>> GetMeetupLocationsAsync(Guid id)
        {
            string getMeetupLocationsQuery = @"SELECT address.* 
                                                FROM Addresses address,
                                                AdvertisementsAddresses adAddress
                                                WHERE adAddress.AdvertisementId = @AdvertisementId
                                                AND address.AddressId = adAddress.AddressId";

            using (var databaseConnection = Client.CreateConnection())
            {
                var meetupLocations = await databaseConnection.QueryAsync<Address>(getMeetupLocationsQuery, new { AdvertisementId = id }).ConfigureAwait(false);

                return meetupLocations;
            }
        }

        public async Task<IEnumerable<Game>> GetProductsAsync(Guid id)
        {
            string getAdGamesQuery = @"SELECT *
                                        FROM Games game
                                        INNER JOIN ProductInformation product
                                        ON game.ProductInformationId = product.ProductInformationId
                                        INNER JOIN PricingInformation pricing
                                        ON product.PricingInformationId = pricing.PricingInformationId
                                        WHERE AdvertisementId = @AdvertisementId;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var games = new List<Game>();

                //Load games data.
                var gamesData = await databaseConnection.QueryAsync(getAdGamesQuery, new { AdvertisementId = id }).ConfigureAwait(false);
                foreach (var gameData in gamesData)
                {
                    Game game = DynamicTranslator.TranslateGame(gameData);
                    game.PricingInformation = DynamicTranslator.TranslatePricingInformation(gameData);

                    games.Add(game);
                }

                return games;
            }
        }
        public async Task<User> GetAdOwnerAsync(Guid id)
        {
            string getAdOwnerQuery = @"SELECT owner.* 
                                        FROM Users owner,
                                        UsersAdvertisements ua
                                        WHERE ua.AdvertisementId = @AdvertisementId
                                        AND owner.UserId = ua.UserId;";

            string getUserAddressesAndContactsQuery = @"SELECT address.* 
                                                        FROM Addresses address,
                                                        UsersAddresses ua
                                                        WHERE ua.UserId = @UserId
                                                        AND ua.AddressId = address.AddressId;

                                                        SELECT contact.* 
                                                        FROM ContactInformation contact,
                                                        UsersContactInformation uc
                                                        WHERE uc.UserId = @UserId
                                                        AND uc.ContactInformationId = contact.ContactInformationId;

                                                        SELECT * FROM Feedbacks WHERE UserId = @UserId;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var command = new CommandDefinition(getAdOwnerQuery, new { AdvertisementId = id });

                //Load user.
                var userData = await databaseConnection.QuerySingleOrDefaultAsync(command).ConfigureAwait(false);

                //No results found.
                if(userData == null)
                {
                    return null;
                }

                var user = DynamicTranslator.TranslateUser(userData);

                var mapper = await databaseConnection.QueryMultipleAsync(getUserAddressesAndContactsQuery, new { UserId = user.UserId }).ConfigureAwait(false);

                //Load user addresses.
                var userAddresses = await mapper.ReadAsync<Address>().ConfigureAwait(false);
                user.Addresses.AddRange(userAddresses);

                //Load user contact information.
                var userContacts = await mapper.ReadAsync<ContactInformation>().ConfigureAwait(false);
                user.ContactInformation.AddRange(userContacts);

                //Load user feedbacks.
                var userFeedbacks = await mapper.ReadAsync().ConfigureAwait(false);
                foreach(var userFeedback in userFeedbacks)
                {
                    Feedback feedback = DynamicTranslator.TranslateFeedback(userFeedback);
                    feedback.User = user;
                    feedback.Owner = new User();

                    user.Feedbacks.Add(feedback);
                }

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
                        OwnerID = advertisement.Owner.UserId,
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

        #endregion
    }
}
