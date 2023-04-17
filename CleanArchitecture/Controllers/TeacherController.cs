using BusinessLayer.Repository;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepo _Teacher;

        public TeacherController(ITeacherRepo teacher)
        {
            _Teacher = teacher;
        }

        [HttpPost("AddTeachers")]
        public IActionResult AddTeacher(Teacher obj)
        {
            _Teacher.AddTeacher(obj);
            return Ok(obj);
        }

        [HttpGet("GetAllTeacher")]
        public async Task<IActionResult> AllTeacher()
        {
            var teacher = await _Teacher.GetAllTeachers();
            return Ok(teacher);
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken(int i)
        {
            var token = await _Teacher.CreateToken(i);
            return Ok(token);
        }

        [HttpPut("UpdateStudentGrade"), Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateDetails(int Id, string Grade, int @class)
        {
            return Ok(await _Teacher.UpdateDetails(Id, Grade, @class, User.Identity.Name));
        }
    }
}