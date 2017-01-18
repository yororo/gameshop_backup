using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string actualPassword);
    }
}
