using BusinessLayer.Repository;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositortImplementation
{
    public class TeacherImple : ITeacherRepo
    {
        private readonly CleanDbContext _db;
        private readonly IConfiguration _configuration;

        public TeacherImple(CleanDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        //?---------------------------------Add Teacher----------------------------
        public async Task AddTeacher(Teacher obj)
        {
            try
            {
                _db.Teachers.Add(obj);
                _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //?-----------------------------------View Teacher----------------------------------
        //
        public async Task<IList<Teacher>> GetAllTeachers()
        {
            return (await _db.Teachers.ToListAsync());
        }

        //?---------------------Create Token -----------------------------------------------

        public async Task<String> CreateToken(int id)
        {
            var role = _db.Teachers.FirstOrDefault(_db => _db.ID == id);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , role.Name),
                new Claim(ClaimTypes.Role , role.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: cred
                    );

            var JwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return JwtToken;
        }

        //?--------------------------Update Details of the Student Grade and Class-------------------------------

        public async Task<Student> UpdateDetails(int Id, string Grade, int @class, string updatesBy)
        {
            try
            {
                if (Id == null)
                {
                    throw new ArgumentNullException("ID can't be Null");
                }

                Student stu = new Student()
                {
                    Id = Id,
                    Grade = Grade,
                    Class = @class,
                };

                _db.StudentDetails.Update(stu);
                _db.SaveChangesAsync();

                return stu;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}