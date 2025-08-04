using Document_Processing_System.DAL;
using Document_Processing_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Document_Processing_System.BL
{
    public class DocumentDetailBL : IDocumentDetailBL
    {
        private readonly DataContext _context;

        public DocumentDetailBL(DataContext context) 
        { 
            _context = context;
           
        }
        public async Task<List<DocumentDetailDTO>> GetDocumentDetails()
        {
            return await _context.DocumentDetails
             .Select(d => new DocumentDetailDTO
             {
                 FileName = d.FileName,
                 Status = d.Status,
                 UploadedDate = d.UploadedDate,
                 WordCount = d.WordCount

             }).ToListAsync();

        }

        public async Task<List<DocumentDetailDTO>> UploadDocuments(List<IFormFile> files)
        {
            var result = new List<DocumentDetailDTO>();



            foreach (var file in files)
            {
                if(file.Length > 0 && Path.GetExtension(file.FileName).ToLower() == ".pdf" && file.Length < 10 * 1024 * 1024)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var hash = Convert.ToBase64String(SHA256.Create().ComputeHash(content));
                    var (text, wordCount) = DocumentDetailHelpers.ExtractTextAndWordCount(content);


                    if (_context.DocumentDetails.Any(d => d.Hash == hash))
                    {
                        result.Add(new DocumentDetailDTO 
                        
                        { 
                            FileName = file.FileName, 
                            Status = "Duplicate",
                            UploadedDate = DateTime.UtcNow,
                            WordCount = wordCount,
                            Text = text

                        });
                        continue;
                    }
                    var document = new DocumentDetail
                    {
                        FileName = file.FileName,
                        Content = content,
                        Hash = hash,
                        Status = "Queued",
                        UploadedDate = DateTime.UtcNow,
                        WordCount = wordCount,
                        Text = text
                    };

                    _context.DocumentDetails.Add(document);
                    await _context.SaveChangesAsync();

                    result.Add(new DocumentDetailDTO
                    {
                        FileName =file.FileName,
                        Status = "Queued",
                        UploadedDate= DateTime.UtcNow,
                        WordCount = wordCount,
                        Text = text
                    });

                }

                else
                {
                    result.Add(new DocumentDetailDTO
                    { 
                        FileName = file.FileName,
                        Status = "Invalid File",
                        UploadedDate = DateTime.UtcNow,
                        WordCount = 0,
                        Text = "File already exists"

                    });
                }
            }
            return result;
        }
    }
}
