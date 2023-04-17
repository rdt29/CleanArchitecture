using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface ITeacherRepo
    {
        public Task AddTeacher(Teacher obj);

        public Task<IList<Teacher>> GetAllTeachers();

        public Task<String> CreateToken(int id);

        public Task<Student> UpdateDetails(int Id, string Grade, int @class, string updatesBy);
    }
}