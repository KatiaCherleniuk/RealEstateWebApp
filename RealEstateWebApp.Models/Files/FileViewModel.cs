using System;

namespace RealEstateWebApp.Models.Files
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FileName  { get; set; }
        public bool IsDeleted  { get; set; }
        public DateTime CreatedAt  { get; set; }
        public string CreatorName  { get; set; }
    }
}