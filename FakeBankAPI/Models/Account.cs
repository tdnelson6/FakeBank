using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBankAPI.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public double InterestRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /*[ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }*/

    }
}
