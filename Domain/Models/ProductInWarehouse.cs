using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductInWarehouse
    {
        public int ProductInWarehouseId { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
