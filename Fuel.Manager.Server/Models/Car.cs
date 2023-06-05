namespace Fuel.Manager.Server.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public int Version { get; set; }

        public IList<Employee> Employees { get; set; }

        public Car()
        {
            Employees = new List<Employee>();
        }
    }
}
