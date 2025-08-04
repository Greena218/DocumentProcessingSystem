using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Processing_System.Entities
{
    public class DocumentDetail
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string Hash {  get; set; }
        public string Status {get; set; }
        public DateTime UploadedDate { get; set; }
        public int WordCount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }
    }
}