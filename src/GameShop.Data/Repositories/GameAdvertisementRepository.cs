using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Providers;
using GameShop.Data.Translators;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Data.Providers.Interfaces;

namespace GameShop.Data.Repositories
{
    public class GameAdvertisementRepository : Repository, IGameAdvertisementRepository
    {
        public GameAdvertisementRepository(IDatabaseProviderClient databaseProviderClient) 
            : base(databaseProviderClient)
        {
           
        }

        #region IAdvertisementAsyncRepository Implementation

        public async Task<Advertisement<Game>> FindByIdAsync(Guid id)
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
                Advertisement<Game> advertisement = DynamicTranslator.TranslateAdvertisement<Game>(advertisementData);

                //Load advertisement products.
                var products = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.Products.AddRange(products);

                //Load advertisement owner. Full.
                advertisement.Owner = await GetOwnerAsync(advertisement.AdvertisementId).ConfigureAwait(false);

                //Load meetup locations.
                var meetupLocations = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.MeetupLocations.AddRange(meetupLocations);

                return advertisement;
            }
        }

        public async Task<Advertisement<Game>> FindByFriendlyIdAsync(string id)
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
                Advertisement<Game> advertisement = DynamicTranslator.TranslateAdvertisement<Game>(advertisementData);
                advertisement.Owner = DynamicTranslator.TranslateUser(advertisementData);
                
                //Load advertisement products.
                var products = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.Products.AddRange(products);

                //Load advertisement owner. Full.
                advertisement.Owner = await GetOwnerAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                
                //Load meetup locations.
                var meetupLocations = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                advertisement.MeetupLocations.AddRange(meetupLocations);

                return advertisement;
            }
        }

        public async Task<IEnumerable<Advertisement<Game>>> FindByTitleAsync(string title)
        {
            string findByTitleQuery = @"SELECT * FROM Advertisements ad
                                        INNER JOIN Users owner
                                        ON ad.OwnerId = owner.UserId
                                        WHERE ad.Title LIKE @Title;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var advertisements = new List<Advertisement<Game>>();

                var advertisementsData = await databaseConnection.QueryAsync(findByTitleQuery, new { Title = string.Format("%{0}%", title) }).ConfigureAwait(false);
                foreach (var advertisementData in advertisementsData)
                {
                    //Instantiate.
                    Advertisement<Game> advertisement = DynamicTranslator.TranslateAdvertisement<Game>(advertisementData);
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
        public async Task<IEnumerable<Advertisement<Game>>> GetAllAsync()
        {
            string getAdsWithOwnerQuery = @"SELECT * 
                                            FROM Advertisements ad
                                            INNER JOIN Users owner
                                            ON ad.OwnerId = owner.UserId;";

            using (var databaseConnection = Client.CreateConnection())
            {
                var advertisements = new List<Advertisement<Game>>();

                //Load advertisements.
                var advertisementsData = await databaseConnection.QueryAsync(getAdsWithOwnerQuery).ConfigureAwait(false);
                foreach (var advertisementData in advertisementsData)
                {
                    Advertisement<Game> advertisement = DynamicTranslator.TranslateAdvertisement<Game>(advertisementData);
                    //Load advertisement owner. Partial.
                    advertisement.Owner = DynamicTranslator.TranslateUser(advertisementData);

                    //Load games.
                    //var games = await GetProductsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                    //advertisement.Products.AddRange(games);

                    ////Load meetup locations.
                    //var meetupLocationsData = await GetMeetupLocationsAsync(advertisement.AdvertisementId).ConfigureAwait(false);
                    //advertisement.MeetupLocations.AddRange(meetupLocationsData);

                    advertisements.Add(advertisement);
                }

                string getAdGamesQuery = @"SELECT *
                                        FROM Games game
                                        INNER JOIN ProductInformation product
                                        ON game.ProductInformationId = product.ProductInformationId
                                        INNER JOIN PricingInformation pricing
                                        ON product.PricingInformationId = pricing.PricingInformationId
                                        WHERE AdvertisementId = '{0}';";

                var queryBuilder = new StringBuilder();

                //Batch query
                //Build batch query for products.
                foreach(var advertisement in advertisements)
                {
                    queryBuilder.AppendFormat(getAdGamesQuery, advertisement.AdvertisementId);
                }

                //Map games.
                var gameMapper = await databaseConnection.QueryMultipleAsync(queryBuilder.ToString()).ConfigureAwait(false);
                foreach(var advertisement in advertisements)
                {
                    var gamesData = await gameMapper.ReadAsync();
                    foreach (var data in gamesData)
                    {
                        var game = DynamicTranslator.TranslateGame(data);
                        game.PricingInformation = DynamicTranslator.TranslatePricingInformation(data);

                        advertisement.Products.Add(game);
                    }
                }

                //Clear query.
                queryBuilder.Clear();

                string getMeetupLocationsQuery = @"SELECT address.* 
                                                FROM Addresses address,
                                                AdvertisementsAddresses adAddress
                                                WHERE adAddress.AdvertisementId = '{0}'
                                                AND address.AddressId = adAddress.AddressId;";

                //Batch query
                //Build batch query for meetup locations.
                foreach (var advertisement in advertisements)
                {
                    queryBuilder.AppendFormat(getMeetupLocationsQuery, advertisement.AdvertisementId);
                }

                //Map meetup locations.
                var addressMapper = await databaseConnection.QueryMultipleAsync(queryBuilder.ToString()).ConfigureAwait(false);
                foreach (var advertisement in advertisements)
                {
                    var meetupLocationsData = await addressMapper.ReadAsync();
                    foreach (var data in meetupLocationsData)
                    {
                        var meetupLocation = DynamicTranslator.TranslateAddress(data);

                        advertisement.MeetupLocations.Add(meetupLocation);
                    }
                }

                return advertisements;
            }
        }

        public async Task<IEnumerable<Advertisement<Game>>> GetAllDeepAsync()
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
                var advertisements = new List<Advertisement<Game>>();

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
                    Advertisement<Game> advertisement = DynamicTranslator.TranslateAdvertisement<Game>(advertisementData);

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
                                                AND address.AddressId = adAddress.AddressId;";

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
        public async Task<User> GetOwnerAsync(Guid id)
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

        public async Task<int> AddAsync(Advertisement<Game> advertisement)
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

        public async Task<int> UpdateAsync(Guid id, Advertisement<Game> advertisement)
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

        public async Task<IEnumerable<Advertisement<Game>>> GetByGameReleaseDateAsync(DateTime releaseDate)
        {
            string getByGameReleaseDateQuery = @"SELECT * 
                                                FROM Advertisements a
                                                INNER JOIN ProductInformation p
                                                ON a.AdvertisementId = p.AdvertisementId
                                                INNER JOIN Games g
                                                ON p.ProductInformationId = g.ProductInformationId
                                                WHERE g.ReleaseDate > @ReleaseDate";

            using (var databaseConnection = Client.CreateConnection())
            {
                var advertisementsData = await databaseConnection.QueryAsync(getByGameReleaseDateQuery, new { ReleaseDate = releaseDate });
                var advertisements = new List<Advertisement<Game>>();
                var games = new List<Game>();

                foreach (var data in advertisementsData)
                {
                    Advertisement<Game> advertisement = DynamicTranslator.TranslateAdvertisement<Game>(data);
                    var game = DynamicTranslator.TranslateGame(data);

                    games.Add(game);
                    advertisement.Products.AddRange(games);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        public async Task<IEnumerable<Advertisement<Game>>> GetByGamingPlatformAsync(GamingPlatform gamingPlatform)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Advertisement<Game>>> GetByGameGenreAsync(GameGenre gameGenre)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
