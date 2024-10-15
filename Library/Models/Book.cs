using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Genre { get; set; }
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Available Copies is required")]
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }

    }
}
