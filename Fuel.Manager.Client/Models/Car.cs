using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel.Manager.Client.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public int Version { get; set; }
    }
}
