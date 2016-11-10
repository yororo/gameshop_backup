using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> FindUserById(Guid id);
        Task<User> FindUser(Predicate<User> predicate);
    }
}
