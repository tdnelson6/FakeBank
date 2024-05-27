using System.ComponentModel.DataAnnotations;

namespace FakeBankAPI.Models.DTOs
{
    public class AccountCreateDTO
    {
        //[Required]
        //[MinLength(10)] [MaxLength(10)]
        public int AccountNumber { get; set; }
        public string ?AccountType { get; set; }
        public double Balance { get; set; }
        public string ?Currency { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string ?Name { get; set; }
        public double InterestRate { get; set; }
    }
}
