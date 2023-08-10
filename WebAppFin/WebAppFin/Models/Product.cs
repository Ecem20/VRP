using System.ComponentModel.DataAnnotations;

namespace WebAppFin.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name ="Product Name")]
        public string ProductName { get; set; } = null;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
