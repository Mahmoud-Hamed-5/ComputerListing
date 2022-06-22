using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Models
{

    public class UserLoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [Required]
        public string Password { set; get; }
    }

    public class UserDTO : UserLoginDTO
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { set; get; }
    }
}
