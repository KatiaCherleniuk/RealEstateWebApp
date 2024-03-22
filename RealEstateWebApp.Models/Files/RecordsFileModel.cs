namespace RealEstateWebApp.Models.Files
{
    public class RecordsFileModel : FileBaseModel
    {
        public int RecordId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}