using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace pdfviewer.Controllers
{
    [Authorize]
    public class PdfViewerController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ICipherService _cipherService;
        
        public PdfViewerController(IWebHostEnvironment env, ICipherService cipherService)
        {
            _env = env;
            _cipherService = cipherService;
        }
        public IActionResult Index()
        {
            var stringKeyData = "sample.pdf"; //this is just a file name for now, can be any information which needs to be passed in prod environment
            var model = new DataModel()
            {
                Key = _cipherService.Encrypt(stringKeyData)
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult GetPdfByKey(string symmetricKey)
        {
            var keyData = _cipherService.Decrypt(symmetricKey);
            var path = Path.Join(_env.ContentRootPath, keyData);
            return new FileStreamResult(new FileStream(path, FileMode.Open), "application/pdf");
        }

        //This is for direct download for trusted clients
        public IActionResult GetPdf()
        {
            var path = Path.Join(_env.ContentRootPath, "sample.pdf");

            var stream = new FileStream(path, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public IActionResult GetCommentsHtml()
        {
            var path = Path.Join(_env.ContentRootPath, "comments-section.html");
            var stream = new FileStream(path, FileMode.Open);
            return new FileStreamResult(stream, "text/html");
        }
    }
}
