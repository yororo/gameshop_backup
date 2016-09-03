using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    /// <summary>
    /// Base class for repositories.
    /// </summary>
    public abstract class Repository
    {
        #region Properties

        /// <summary>
        /// Database connection provider.
        /// </summary>
        public IDatabaseConnectionProvider DatabaseConnectionProvider { get; set; }

        #endregion Properties

        #region Constructors

        public Repository(IDatabaseConnectionProvider provider)
        {
            DatabaseConnectionProvider = provider;
        }

        #endregion Constructors

        #region Execute

        /// <summary>
        /// Execute a database command.
        /// </summary>
        /// <param name="commandText">Database command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="commandType">Command type.</param>
        protected virtual void Execute(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var databaseConnection = DatabaseConnectionProvider.Get())
            {
                databaseConnection.Open();

                using (var command = buildCommand(databaseConnection, commandText, parameters, commandType))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion Execute

        #region ExecuteRead

        /// <summary>
        /// Execute command and return a single result of given type
        /// </summary>
        /// <typeparam name="T">Expected type to return.</typeparam>
        /// <param name="commandText">Database command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>Instance of given type.</returns>
        protected virtual T ExecuteRead<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            return executeReadEnumerable<T>(commandText, parameters, commandType).FirstOrDefault();
        }

        /// <summary>
        /// Execute command and return an list of given type.
        /// </summary>
        /// <typeparam name="T">Expected type to return.</typeparam>
        /// <param name="commandText">Database command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>IEnumerable list of given type.</returns>
        protected virtual IEnumerable<T> ExecuteReadList<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            return executeReadEnumerable<T>(commandText, parameters, commandType).ToList();
        }

        /// <summary>
        /// Execute command and yield list of given type. This will return no result until needed. (e.g. foreach loop, for loop, etc.).
        /// </summary>
        /// <typeparam name="T">Expected type to return.</typeparam>
        /// <param name="commandText">Database command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>IEnumerable list of given type.</returns>
        protected virtual IEnumerable<T> ExecuteYieldList<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            return executeReadEnumerable<T>(commandText, parameters, commandType);
        }

        /// <summary>
        /// Execute command and return an enumerable result of given type.
        /// </summary>
        /// <typeparam name="T">Expected type to return.</typeparam>
        /// <param name="commandText">Database command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>IEnumerable list of given type.</returns>
        private IEnumerable<T> executeReadEnumerable<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var databaseConnection = DatabaseConnectionProvider.Get())
            {
                databaseConnection.Open();

                using (var command = buildCommand(databaseConnection, commandText, parameters, commandType))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Call abstract MapResult method which is implemented in child repository to map database result to domain objects.
                            yield return (T)MapResult(reader);
                        }
                    }
                }
            }
        }

        #endregion ExecuteRead

        #region Functions

        /// <summary>
        /// Build a database command based on the DatabaseConnection with parameters.
        /// </summary>
        /// <param name="databaseConnection">IDbConnection.</param>
        /// <param name="commandText">Database command.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="commandType">Command type.</param>
        /// <returns>Instance of IDbCommand.</returns>
        private IDbCommand buildCommand(IDbConnection databaseConnection, string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            IDbCommand command = databaseConnection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
            {
                addParametersFromDictionary(command, parameters);
            }

            return command;
        }

        /// <summary>
        /// Add parameters to a database command.
        /// </summary>
        /// <param name="command">Database command.</param>
        /// <param name="paramaters">Parameters.</param>
        private void addParametersFromDictionary(IDbCommand command, IDictionary<string, object> paramaters)
        {
            foreach (var parameterKeyValue in paramaters)
            {
                IDbDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = parameterKeyValue.Key;
                parameter.Value = parameterKeyValue.Value;

                command.Parameters.Add(parameter);
            }
        }

        #endregion Functions

        #region Abstract Methods

        /// <summary>
        /// Map IDataRecord from DataReader to domain object.
        /// </summary>
        /// <param name="record">IDataRecord from DataReader. A single data records represents a single row in the database.</param>
        /// <returns>Mapped domain object.</returns>
        protected abstract object MapResult(IDataRecord record);

        #endregion Abstract Methods
    }
}
