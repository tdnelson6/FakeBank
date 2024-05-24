using System.ComponentModel.DataAnnotations;

namespace FakeBankAPI.Models.DTOs
{
    public class AccountUpdateDTO
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}
