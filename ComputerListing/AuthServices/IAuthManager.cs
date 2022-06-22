using ComputerListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.AuthServices
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserLoginDTO userDTO);

        Task<string> CreateToken();
    }
}
