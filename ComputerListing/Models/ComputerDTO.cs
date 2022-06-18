using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerListing.Models
{
    public class CreateComputerDTO
    {
        
        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "Manufacturer Name Is Too Long!")]
        public string Manufacturer { set; get; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Model Name Is Too Long!")]
        public string Model { set; get; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Proccessor Name Is Too Long!")]
        public string Proccessor { set; get; }

        [Required]
        public int RAM { set; get; }
    }

    public class ComputerDTO : CreateComputerDTO
    {
        public int Id { get; set; }

        public IList<AccessoryDTO> Accessories { set; get; }
    }
}
