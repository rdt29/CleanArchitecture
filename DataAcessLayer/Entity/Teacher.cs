using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entity
{
    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}