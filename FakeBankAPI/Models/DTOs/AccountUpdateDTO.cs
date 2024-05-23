using System.ComponentModel.DataAnnotations;

namespace FakeBankAPI.Models.DTOs
{
    public class AccountUpdateDTO
    {
        [Required]
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
