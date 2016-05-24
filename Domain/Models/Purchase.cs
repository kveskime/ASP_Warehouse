using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Purchase : BaseEntity
    {
        public int PurchaseId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreationDateTime { get; set; }



        public virtual List<ProductInPurchase> ProductsInPurchase { get; set; } = new List<ProductInPurchase>();
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}