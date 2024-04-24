using System;

namespace RealEstateWebApp.Models.Files
{
    public class FileCreateModel
    {
        public int Id { get; set; }
        public int RecordId  { get; set; }
        public string FilePath  { get; set; }
        public string FileName  { get; set; }
        public bool IsDeleted  { get; set; }
        public DateTime CreatedAt  { get; set; }
        public int CreatedBy  { get; set; }
        public string ContentType  { get; set; }
        public bool IsMain  { get; set; }
    }
}