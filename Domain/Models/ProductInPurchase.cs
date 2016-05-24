using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductInPurchase
    {
        public int ProductInPurchaseId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        public int Quantity { get; set; }


        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}