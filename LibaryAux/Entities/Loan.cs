using LibaryAux.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Entities
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public DateTime LoanDate { get; set; }
        public int LoanPeriod { get; set; }

    }
}
