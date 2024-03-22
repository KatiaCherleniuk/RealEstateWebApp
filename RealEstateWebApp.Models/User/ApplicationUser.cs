using System;

namespace RealEstateWebApp.Models.User
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public DateTime? LockoutEnd  { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount  { get; set; }
        public bool IsBlocked { get; set; }
    }
}