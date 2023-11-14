using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCTOBER.Shared.DTO
{
    public class StudentDTO
    {
        [Precision(8)]
        public int StudentId { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string? Salutation { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? FirstName { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? StreetAddress { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Employer { get; set; }
        public DateTime RegistrationDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

        [Precision(8)]
        public int SchoolId { get; set; }

    }
}