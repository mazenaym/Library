using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Return
    {
        [Key]
        public int ReturnID { get; set; }
        public int CheckoutID { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PenaltyAmount { get; set; }
        public required Checkout Checkout { get; set; }


    }
}
