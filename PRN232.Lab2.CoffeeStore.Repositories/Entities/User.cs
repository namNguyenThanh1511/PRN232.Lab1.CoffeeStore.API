namespace PRN232.Lab2.CoffeeStore.Repositories.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }


        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public enum Role
    {
        Admin,
        Staff,
        Customer
    }
}
