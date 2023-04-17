using DataAcessLayer.DTO;
using DataAcessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface IStudentRepo
    {
        Task<IList<Student>> GetAllStudent();

        Task AddStudent(Student obj);
    }
}