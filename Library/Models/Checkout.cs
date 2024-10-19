using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Checkout
    {
        [Key]
        public int CheckoutID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public required DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public required Book Book { get; set; }
        public required Member Member { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int? Penalty { get; set; }



    }
}
