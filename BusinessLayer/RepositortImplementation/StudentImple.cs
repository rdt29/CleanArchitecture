using BusinessLayer.Repository;
using DataAcessLayer.DTO;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositortImplementation
{
    public class StudentImple : IStudentRepo
    {
        private readonly CleanDbContext _db;

        public StudentImple(CleanDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Student>> GetAllStudent()
        {
            //Log.Information("Get All Student");
            try
            {
                //Log.Debug("Debug");
                //throw new Exception("Coustum exception");
                var stu = await _db.StudentDetails.ToListAsync();
                return stu;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddStudent(Student obj)
        {
            try
            {
                _db.StudentDetails.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}