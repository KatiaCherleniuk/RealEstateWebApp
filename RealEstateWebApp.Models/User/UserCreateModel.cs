namespace RealEstateWebApp.Models.User
{
    public class UserCreateModel : ApplicationUser
    {
        public string RoleName { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}