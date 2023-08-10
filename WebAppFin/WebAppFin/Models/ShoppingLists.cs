using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFin.Models
{
    public class ShoppingLists
    {
        [Key]
        public int ListId { get; set; }

        [Display(Name ="List Name")]
        public string ListName { get; set; }
        public string UserName { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
