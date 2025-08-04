using Document_Processing_System.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Document_Processing_System.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentDetailsController : ControllerBase
    {
        private readonly IDocumentDetailBL _documentDetailBL;

        public DocumentDetailsController(IDocumentDetailBL documentDetailBL)
        {
            _documentDetailBL = documentDetailBL;
        }
        [HttpGet("GetAllDocument")]
        public async Task<IActionResult> GetDocdetails()
        {
            return Ok(await _documentDetailBL.GetDocumentDetails());
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadDocuments(List<IFormFile> files)
        {
            return Ok(await _documentDetailBL.UploadDocuments(files));
        }
    }
}
