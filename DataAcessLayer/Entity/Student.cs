﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entity
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string UpdatesBy { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public int Class { get; set; } = 0;
    }
}