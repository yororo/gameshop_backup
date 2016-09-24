using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Requests
{
    public class SearchAdRequest : APIRequest
    {
        #region Fields
        private string _searchStr;
        #endregion

        #region Properties
        public string SearchStr
        {
            get
            {
                return _searchStr;
            }

            set
            {
                _searchStr = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public SearchAdRequest()
        {
            SearchStr = string.Empty;
        }
        #endregion

        #region Private Methods
        
        #endregion

        #region Public Methods

        #endregion
    }
}
