using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Penalty
    {
        [Key]
        public int PenaltyID { get; set; }
        public int MemberID { get; set; }
        public int CheckoutID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaidDate { get; set; }


    }
}
