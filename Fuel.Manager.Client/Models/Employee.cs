using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel.Manager.Client.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmployeeNo { get; set; }
        public bool IsAdmin { get; set; }
        public int Version { get; set; }
    }
}
