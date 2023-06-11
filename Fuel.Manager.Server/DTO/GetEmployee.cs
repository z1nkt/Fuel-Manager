namespace Fuel.Manager.Server.DTO
{
    public class GetEmployee
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
