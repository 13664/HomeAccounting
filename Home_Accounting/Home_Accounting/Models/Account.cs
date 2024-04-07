using Home_Accounting.Models;
using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public  string  Name  { get; set; }
        public double Balance { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        
    }
}
