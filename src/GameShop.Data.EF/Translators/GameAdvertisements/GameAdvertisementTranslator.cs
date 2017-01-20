using System.Collections.Generic;
using GameShop.Contracts.Entities;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Translators
{
    internal static class GameAdvertisementTranslator
    {
        #region To Entity

        public static EFEntities.Games.GameAdvertisement ToGameAdvertisementEntity(this Advertisement<Game> contractAdGame)
        {
            // Guard clause.
            if (contractAdGame == null)
            {
                return null;
            }

            var efGame = new EFEntities.Games.GameAdvertisement();

            efGame.CreatedDate = contractAdGame.CreatedDate;
            efGame.Description = contractAdGame.Description;
            efGame.FriendlyId = contractAdGame.FriendlyId;

            foreach(Game game in contractAdGame.Products)
            {
                efGame.Games.Add(game.ToGameEntity());
            }

            efGame.Id = contractAdGame.Id;
            //fGame.MeetupInformation = contractAdGame.MeetupInformation;
            efGame.ModifiedDate = contractAdGame.ModifiedDate;
            //efGame.Owner = contractAdGame.Owner;
            efGame.State = contractAdGame.State;
            efGame.Title = contractAdGame.Title;

            return efGame;
        }

        #endregion To Entity
        
        #region To Contract

        public static Advertisement<Game> ToGameAdvertisementContract(this EFEntities.Games.GameAdvertisement efAd)
        {
            if (efAd == null)
            {
                return null;
            }

            GameAdvertisement contractGame = new GameAdvertisement();

            contractGame.CreatedDate = efAd.CreatedDate.Value;
            contractGame.Description = efAd.Description;
            contractGame.FriendlyId = efAd.FriendlyId;

            foreach(EFEntities.Games.Game game in efAd.Games)
            {
                contractGame.Products.Add(game.ToGameContract());
            }

            contractGame.Id = efAd.Id;
            //fGame.MeetupInformation = contractAdGame.MeetupInformation;
            contractGame.ModifiedDate = efAd.ModifiedDate.Value;
            //efGame.Owner = contractAdGame.Owner;
            contractGame.State = efAd.State;
            contractGame.Title = efAd.Title;

            return contractGame;
        }
        
        #endregion To Contract
    }
}