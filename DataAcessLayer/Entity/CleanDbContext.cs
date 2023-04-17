using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entity
{
    public class CleanDbContext : DbContext
    {
        public CleanDbContext(DbContextOptions<CleanDbContext> options) : base(options)
        {
        }

        public DbSet<Student> StudentDetails { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}