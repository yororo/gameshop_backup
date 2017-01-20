using System.Collections.Generic;

using GameShop.Contracts.Entities;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Translators
{
    internal static class GameTradingInformationTranslator
    {

        #region To Entity
        
        public static EFEntities.Games.GameTradingInformation ToGameTradingInformationEntity(this TradingInformation tradingInformation)
        {
            // Guard clause.
            if (tradingInformation == null)
            {
                return null;
            }

            var efGameTradingInformation = new EFEntities.Games.GameTradingInformation();

            efGameTradingInformation.CashAmountToAdd = tradingInformation.CashAmountWillingToAdd;
            efGameTradingInformation.Currency = tradingInformation.Currency;
            efGameTradingInformation.IsOwnerWillingToAddCash = tradingInformation.IsOwnerWillingToAddCash;
            efGameTradingInformation.IsOwnerWillingToReceiveCash = tradingInformation.IsOwnerWillingToReceiveCash;
            efGameTradingInformation.ReasonForSelling = tradingInformation.ReasonForTrading;
            efGameTradingInformation.TradeNotes = tradingInformation.TradeNotes;
            efGameTradingInformation.TradingPrice = tradingInformation.TradingPrice;
            efGameTradingInformation.CreatedDate = tradingInformation.CreatedDate;
            efGameTradingInformation.ModifiedDate = tradingInformation.ModifiedDate;

            return efGameTradingInformation;
        }

        #endregion To Entity
        
        #region To Contract

        public static TradingInformation ToGameTradingInformationContract(this EFEntities.TradingInformation efTradingInfo)
        {
            if (efTradingInfo == null)
            {
                return null;
            }

            TradingInformation tradingInfoContract = new TradingInformation();

            tradingInfoContract.CashAmountWillingToAdd = efTradingInfo.CashAmountToAdd;
            tradingInfoContract.CreatedDate = efTradingInfo.CreatedDate.Value;
            tradingInfoContract.Currency = efTradingInfo.Currency;
            tradingInfoContract.Id = efTradingInfo.Id;
            tradingInfoContract.IsOwnerWillingToAddCash = efTradingInfo.IsOwnerWillingToAddCash;
            tradingInfoContract.IsOwnerWillingToReceiveCash = efTradingInfo.IsOwnerWillingToReceiveCash;
            tradingInfoContract.ModifiedDate = efTradingInfo.ModifiedDate.Value;
            tradingInfoContract.ReasonForTrading = efTradingInfo.ReasonForSelling;
            tradingInfoContract.TradeNotes = efTradingInfo.TradeNotes;
            tradingInfoContract.TradingPrice = efTradingInfo.TradingPrice;

            return tradingInfoContract;
        }

        #endregion To Contract
    }
}