using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Home_Accounting.Models
{
    public class Transaction
    {
        public int Id { get; set; }
       
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public double Amount { get; set; } 
        public string Comment {  get; set; }

        public int TypeId { get; set; }
        public CategoryType Type { get; set; }

        public int CategoryId { get; set; }
        public  Category Category { get; set; }

        



    }
}
