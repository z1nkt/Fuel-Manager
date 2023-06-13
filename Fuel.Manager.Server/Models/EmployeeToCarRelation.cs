namespace Fuel.Manager.Server.Models
{
    public class EmployeeToCarRelation
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public Car Car { get; set; }
    }
}
