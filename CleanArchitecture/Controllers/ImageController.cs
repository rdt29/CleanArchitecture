using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<string> Upload(IFormFile fileupload)
        {
            try
            {
                if (fileupload.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + fileupload.FileName))
                    {
                        fileupload.CopyTo(filestream);
                        filestream.Flush();
                        return ($"Uploaded Done {path + fileupload.FileName}");
                    }
                }
                else

                {
                    return "Error";
                }
            }
            catch (Exception exe)
            {
                return exe.Message;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(string filename)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";

            var filepath = path + filename + ".png";
            if (System.IO.File.Exists(filepath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filepath);
                return File(b, "image/png");
            }
            return BadRequest("not found");
        }
    }
}