﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class AddEmployeeRequest
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Sex { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int? Deptno { get; set; }
    }
}
