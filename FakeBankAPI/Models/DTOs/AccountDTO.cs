using System.ComponentModel.DataAnnotations;

namespace FakeBankAPI.Models.DTOs
{
    public class AccountDTO
    {
        public string Id { get; set; }

        [Required]
        [MinLength(10)] [MaxLength(10)]
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public string UserId { get; set; }
        public string Currency { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public double InterestRate { get; set; }
    }
}
