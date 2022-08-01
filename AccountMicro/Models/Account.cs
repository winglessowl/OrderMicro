using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountMicro.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }    
        public int Funds { get; set; }

        public bool CheckFunds(int price) 
        {
            return this.Funds - price > 0;
        }

        public void ReserveFunds(int price) 
        {
            this.Funds = this.Funds - price;
        }
    }
}
