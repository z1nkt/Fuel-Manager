namespace Fuel.Manager.Server.Models
{
    public class Refuel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Mileage { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public Car Car { get; set; }
        public int Version { get; set; }

        public Refuel()
        {
            Car = new Car();
        }
    }
}
