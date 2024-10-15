using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required long PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }


        
    }
}
