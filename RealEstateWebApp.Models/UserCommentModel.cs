using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApp.Models
{
    public class UserCommentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecordId { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
