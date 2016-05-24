using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Warehouse : BaseEntity
    {
        public int WarehouseId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        public List<ProductInWarehouse> ProductsInWarehouse { get; set; } = new List<ProductInWarehouse>();

    }
}
