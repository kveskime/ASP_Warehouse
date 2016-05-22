using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }

        public int? Rating { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [MaxLength(128)]
        public string Country { get; set; }

        public DeliverySpeed DeliverySpeed { get; set; }

        public virtual List<Purchase> Purchases { get; set; }
    }
    public enum DeliverySpeed
    {
        NextDay,
        Fast,
        Slow
    }
}