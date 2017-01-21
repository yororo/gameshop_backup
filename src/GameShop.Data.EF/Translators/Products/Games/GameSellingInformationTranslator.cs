using System.Collections.Generic;

using GameShop.Contracts.Entities;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Translators
{
    internal static class GameSellingInformationTranslator
    {

        #region To Entity
        
        public static EFEntities.Games.GameSellingInformation ToGameSellingInformationEntity(this SellingInformation sellingInformation)
        {
            // Guard clause.
            if (sellingInformation == null)
            {
                return null;
            }

            var efGameSellingInformation = new EFEntities.Games.GameSellingInformation();

            efGameSellingInformation.Currency = sellingInformation.Currency;
            efGameSellingInformation.ReasonForSelling = sellingInformation.ReasonForSelling;
            efGameSellingInformation.SellingPrice = sellingInformation.SellingPrice;
            efGameSellingInformation.CreatedDate = sellingInformation.CreatedDate;
            efGameSellingInformation.ModifiedDate = sellingInformation.ModifiedDate;

            return efGameSellingInformation;
        }

        #endregion To Entity
        
        #region To Contract

        public static SellingInformation ToGameSellingInformationContract(this EFEntities.SellingInformation efSellingInfo)
        {
            if (efSellingInfo == null)
            {
                return null;
            }

            var sellingInfoContract = new SellingInformation();

            sellingInfoContract.CreatedDate = efSellingInfo.CreatedDate.Value;
            sellingInfoContract.Currency = efSellingInfo.Currency;
            sellingInfoContract.Id = efSellingInfo.Id;
            sellingInfoContract.ModifiedDate = efSellingInfo.ModifiedDate.Value;
            sellingInfoContract.ReasonForSelling = efSellingInfo.ReasonForSelling;
            sellingInfoContract.SellingPrice = efSellingInfo.SellingPrice;

            return sellingInfoContract;
        }

        #endregion To Contract
    }
}