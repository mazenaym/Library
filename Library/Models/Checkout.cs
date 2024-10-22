using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Checkout
    {
        [Key]
        public int CheckoutID { get; set; }

        [DisplayName("Book Name & Author")]
        public int BookID { get; set; }

        [DisplayName("Member Name")]
        public int MemberID { get; set; }
        public required DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public Book? Book { get; set; }
        public Member? Member { get; set; }
        public decimal? Penalty { get; set; }

    }
}
