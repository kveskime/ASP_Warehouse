using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
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
