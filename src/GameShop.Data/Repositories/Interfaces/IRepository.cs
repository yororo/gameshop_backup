using GameShop.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IRepository
    {
        DatabaseConnectionProvider DatabaseConnectionProvider { get; set; }

        void Execute(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text);
        T ExecuteRead<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text);
        IEnumerable<T> ExecuteReadList<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text);
        IEnumerable<T> ExecuteYieldList<T>(string commandText, IDictionary<string, object> parameters = null, CommandType commandType = CommandType.Text);
    }
}
