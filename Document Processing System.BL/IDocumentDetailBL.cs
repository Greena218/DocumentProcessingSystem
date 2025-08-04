using Document_Processing_System.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Processing_System.BL
{
    public interface IDocumentDetailBL
    {
        public Task<List<DocumentDetailDTO>> GetDocumentDetails();
        public Task<List<DocumentDetailDTO>> UploadDocuments(List<IFormFile> files);
    }
}
