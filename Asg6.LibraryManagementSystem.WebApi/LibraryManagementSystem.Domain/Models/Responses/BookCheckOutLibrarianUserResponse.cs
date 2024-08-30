using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class BookCheckOutLibrarianUserResponse
    {

        [Column("fname")]
        [StringLength(255)]
        public string FName { get; set; } = null!;

        [Column("lname")]
        [StringLength(255)]
        public string LName { get; set; } = null!;

        [Column("librarycardnumber")]
        [StringLength(255)]
        public string LibraryCardNumber { get; set; } = null!;

        [Column("librarycardexpireddate")]
        [StringLength(255)]
        public DateOnly LibraryCardExpiredDate { get; set; }

        public int Penalty { get; set; }
        public int CurrentBook {  get; set; }
    }
}
