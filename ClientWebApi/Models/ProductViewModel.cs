using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientWebApi.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public Double Price { get; set; }
        [Required]
        public int Qty { get; set; }
    }
}
