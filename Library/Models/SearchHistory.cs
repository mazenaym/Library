using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class SearchHistory
    {
        [Key] public int SearchID { get; set; }
        public int MemberID { get; set; }
        public required string SearchQuery { get; set; }
        public DateTime SearchTimestamp { get; set; }


    }
}
