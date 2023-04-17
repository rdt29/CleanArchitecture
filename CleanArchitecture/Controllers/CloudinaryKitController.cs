using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinaryKitController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadImg(IFormFile fileupload)
        {
            try
            {
                var cloudinary = new Cloudinary(new Account("dzwma7qcb", "591491493691445", "2bJuC3k7biQk4uZBwVu8Ok9YpPk"));

                // Upload

                var stream = fileupload.OpenReadStream();

                var uploadParams = new ImageUploadParams()
                {
                    //File = new FileDescription(@fileupload),
                    File = new FileDescription(fileupload.Name, stream),
                    PublicId = fileupload.FileName,
                    Folder = "Trail",
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                return Ok(uploadResult);
                //return Ok(fileupload.Name+"      "+ stream);
            }
            catch (Exception)
            {
                throw;
            }
            //Transformation
            //cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(100).Height(150).Crop("fill")).BuildUrl("olympic_flag")
        }

        [HttpPost("path")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                //var fileName = Path.GetFileName(file.FileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}
                return Ok(file);
            }
            else
            {
                return BadRequest("File not uploaded.");
            }
        }
    }
}