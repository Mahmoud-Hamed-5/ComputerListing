using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Data
{
    public class Accessory
    {
        public int Id { set; get; }

        public List<string> AccessoriesList { set; get; }

        [ForeignKey(nameof(Computer))]
        public int ComputerId { set; get; }

        public Computer Computer { set; get; }

    }
}
