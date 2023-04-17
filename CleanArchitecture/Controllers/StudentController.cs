using BusinessLayer.Repository;
using DataAcessLayer.DTO;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepo _stu;

        public StudentController(IStudentRepo stu)
        {
            _stu = stu;
        }

        [HttpPost("ADD Student") , Authorize (Roles = "Teacher")]
        public async Task<IActionResult> AddStudent(StudentDTO stu)
        {
            Student stuobj = new Student()
            {
                Id = stu.Id,
                Name = stu.Name,
            };

            await _stu.AddStudent(stuobj);
            return Ok(stuobj);
        }

        [HttpGet("Gell All Student"), Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetStudent()
        {
          
                return Ok(await _stu.GetAllStudent());
            
            
        }
    }
}