using ComputerListing.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Models
{
    public class CreateAccessoryDTO
    {
        [Required]
        [StringLength(maximumLength:50 , ErrorMessage ="Accessory Name Is Too Long!")]
        public string Name { set; get; }

        [Required]
        public int ComputerId { set; get; }   
    }

    public class UpdateAccessoryDTO : CreateAccessoryDTO
    {

    }

    public class AccessoryDTO : CreateAccessoryDTO
    {
        public int Id { set; get; }

        public ComputerDTO Computer { set; get; }
    }
}
