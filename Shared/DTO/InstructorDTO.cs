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
    public class InstructorDTO
    {
        [Precision(8)]
        public int SchoolId { get; set; }
        [Precision(8)]
        public int InstructorId { get; set; }
        [StringLength(5)]
        public string Salutation { get; set; } = null!;
        [StringLength(25)]
        public string FirstName { get; set; } = null!;
        [StringLength(25)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        public string StreetAddress { get; set; } = null!;
        [StringLength(5)]
        public string Zip { get; set; } = null!;
        [StringLength(15)]
        public string Phone { get; set; } = null!;
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

    }
}