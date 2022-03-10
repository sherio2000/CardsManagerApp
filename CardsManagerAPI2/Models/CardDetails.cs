using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardsManagerAPI2.Models
{
    public class CardDetails
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nVarchar(100)")]
        public string CardHolderName { get; set; }
        [Column(TypeName = "nVarchar(100)")]
        public string CustomerId { get; set; }
        [Column(TypeName = "nVarchar(16)")]
        public string CardNumber { get; set; }
        [Column(TypeName = "nVarchar(5)")]
        public string ExpirationDate { get; set; }
        [Column(TypeName = "nVarchar(3)")]
        public string SecurityCode { get; set; }
    }
}
