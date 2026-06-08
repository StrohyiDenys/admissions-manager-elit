using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbitElit.DataAccess
{
    public class Applicant
    {
        public int Id { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public decimal ExamScore { get; set; }

        public int SchoolNumber { get; set; }
    }
}