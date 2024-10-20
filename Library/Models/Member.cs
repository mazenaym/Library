using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<Checkout> Checkouts { get; set; }
        [NotMapped] // Excludes this property from data annotations
        public string FullName => $"{FirstName} {LastName}";
    }
}
