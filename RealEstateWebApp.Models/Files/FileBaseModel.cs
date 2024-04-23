using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateWebApp.Models.Files
{
    public class FileBaseModel
    {
        public int Id { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsMain { get; set; }

    }
}
