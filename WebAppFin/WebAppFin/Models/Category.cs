using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebAppFin.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int CategoryId { get; set; }

        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
