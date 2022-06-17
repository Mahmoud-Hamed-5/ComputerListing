using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
    }
}
