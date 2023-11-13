using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OCTOBER.Shared.DTO
{
    public class ZipCodeDTO
    {
        [StringLength(5)]
        public string Zip { get; set; } = null!;
        [StringLength(25)]
        public string City { get; set; } = null!;
        [StringLength(2)]
        public char State { get; set; }
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }


    }
}