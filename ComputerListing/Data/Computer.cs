using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Data
{
    public class Computer
    {
        public int Id { get; set; }
        public string Manufacturer { set; get; }
        public string Model { set; get; }
        public string Proccessor { set; get; }

        public int RAM { set; get; }

        public virtual IList<Accessory> Accessories { set; get; }

    }
}
