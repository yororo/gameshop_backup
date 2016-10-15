using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GameShop.Data.Providers.Interfaces;
using System.Data;
using System.Text;
using System.Data.Common;
using GameShop.Contracts.Enumerations;

namespace GameShop.Data.Repositories
{
    public class Repository
    {
        #region Fields

        private DbProviderFactory _databaseProviderFactory;
        private GamingPlatform _gamingPlatform;
        private string _tableName;

        #endregion

        #region Propeorties

        /// <summary>
        /// Database provider factory.
        /// </summary>
        public DbProviderFactory DatabaseProviderFactory
        {
            get
            {
                return _databaseProviderFactory;
            }

            set
            {
                _databaseProviderFactory = value;
            }
        }

        /// <summary>
        /// Name of the repository's database table.
        /// </summary>
        protected string TableName
        {
            get
            {
                return _tableName;
            }

            set
            {
                _tableName = value;
            }
        }

        public GamingPlatform GamingPlatform
        {
            get
            {
                return _gamingPlatform;
            }

            set
            {
                _gamingPlatform = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Repository()
        {

        }

        public Repository(IDatabaseProviderFactory factory)
        {
            DatabaseProviderFactory = factory as DbProviderFactory;

            //Defaults
            TableName = this.GetType().Name;
        }

        public Repository(DbProviderFactory factory)
        {
            DatabaseProviderFactory = factory;

            //Defaults
            TableName = this.GetType().Name;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
