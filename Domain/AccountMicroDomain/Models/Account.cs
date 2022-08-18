using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.AccountMicroDomain.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Funds { get; set; }
        //public Account(string? _name, int _funds) 
        //{
        //    this.Name = _name;
        //    this.SetFunds(_funds);
        //}

        private void SetFunds(int funds)
        {
            ValidateFunds(funds);
            Funds = funds;
        }

        private void ValidateFunds(int funds)
        {
            if (funds < 0)
            {
                throw new Exception("Funds can not be negative");
            }
        }
        public bool CheckFunds(int price)
        {
            return Funds - price > 0;
        }

        public void ReserveFunds(int price)
        {
            Funds = Funds - price;
        }
    }
}
