using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain.Models
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }

        public virtual List<ProductInPurchase> ProductInPurchases { get; set; }
        public virtual List<ProductInWork> ProductInWorks { get; set; } = new List<ProductInWork>();
        public virtual List<ProductInWarehouse> ProductInWarehouses { get; set; }

        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }


    }

}
