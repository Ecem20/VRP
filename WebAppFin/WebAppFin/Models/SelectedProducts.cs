using System.ComponentModel.DataAnnotations;

namespace WebAppFin.Models
{
    public class SelectedProducts
    {
        [Key]
        public int SelectedProductId { get; set; }
        public int ListId { get; set; }
        public string SelectedProductName { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ShoppingLists ShoppingList { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Product? Product { get; set; }

    }
}
