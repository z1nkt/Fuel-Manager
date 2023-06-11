namespace Fuel.Manager.Server.DTO
{
    public class AddEmployee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmployeeNo { get; set; }
        public string IsAdmin { get; set; }
        public int Version { get; set; }
    }

}
