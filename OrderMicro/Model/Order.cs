using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMicro.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        public int IdClient { get; set; }
        public string? Symbol { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string? Status { get; set; }
    }
}
