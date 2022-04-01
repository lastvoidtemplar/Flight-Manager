using System.ComponentModel.DataAnnotations;

namespace FlightManagerWeb.Models
{
    public class TestRegex
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression("^[0-9]{10}$")]
        public string SSN { get; set;}
    }
}