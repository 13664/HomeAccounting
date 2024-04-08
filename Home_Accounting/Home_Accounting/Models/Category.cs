using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Home_Accounting.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        [ValidateNever]
        public CategoryType CategoryType { get; set; }
        //public virtual ICollection<Transaction>? Transactions { get; set; }
       // public ICollection<Transaction> Transactions { get; set; }


       
    }
}
