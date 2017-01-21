using System.Collections.Generic;

using GameShop.Contracts.Entities;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Translators
{
    internal static class GameTranslator
    {

        #region To Entity
        
        public static IEnumerable<EFEntities.Games.Game> ToContracts(this List<Game> games)
        {
            if (games == null)
            {
                return null;
            }

            var efGames = new List<EFEntities.Games.Game>();

            foreach (Game game in games)
            {
                efGames.Add(game.ToGameEntity());
            }

            return efGames;
        }

        public static EFEntities.Games.Game ToEntity(this Game game)
        {
            // Guard clause.
            if (game == null)
            {
                return null;
            }

            var efGame = new EFEntities.Games.Game();

            efGame.CreatedDate = game.CreatedDate;
            efGame.Description = game.Description;
            efGame.Name = game.Name;
            efGame.Id = game.Id;
            efGame.ModifiedDate = game.ModifiedDate;
            efGame.SellingInformation = game.SellingInformation.ToGameSellingInformationEntity();
            efGame.State = game.ProductState;
            efGame.TradingInformation = game.TradingInformation.ToGameTradingInformationEntity();

            return efGame;
        }


        #endregion To Entity
        
        #region To Contract

        public static Game ToGameContract(this EFEntities.Games.Game efGame)
        {
            if (efGame == null)
            {
                return null;
            }

            var gameContract = new Game();

            gameContract.CreatedDate = efGame.CreatedDate.Value;
            gameContract.Description = efGame.Description;
            gameContract.GamingPlatform = efGame.GamePlatform;
            gameContract.Genre = efGame.GameGenre;
            gameContract.Id = efGame.Id;
            gameContract.ModifiedDate = efGame.ModifiedDate.Value;
            gameContract.Name = efGame.Name;
            gameContract.ProductState = efGame.State;
            gameContract.SellingInformation = efGame.SellingInformation.ToGameSellingInformationContract();
            gameContract.TradingInformation = efGame.TradingInformation.ToGameTradingInformationContract();

            return gameContract;
        }
        
        public static IEnumerable<Game> ToGameContracts(this List<EFEntities.Games.Game> efGames)
        {
            if (efGames == null)
            {
                return null;
            }

            var gameContracts = new List<Game>();

            foreach (EFEntities.Games.Game efGame in efGames)
            {
                gameContracts.Add(efGame.ToGameContract());
            }

            return gameContracts;
        }

        #endregion To Contract
    }
}