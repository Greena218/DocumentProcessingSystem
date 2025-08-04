using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Processing_System.Entities
{
    public class DocumentDetailDTO
    {
        public string FileName { get; set; }
        public string Status { get; set; }
        public DateTime UploadedDate { get; set; }

        public int WordCount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }

    }
}
