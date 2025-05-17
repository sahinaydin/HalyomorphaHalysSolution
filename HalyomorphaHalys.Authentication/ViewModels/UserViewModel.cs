namespace HalyomorphaHalys.Authentication.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
