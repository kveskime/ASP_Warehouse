using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        public List<ProductInWarehouse> ProductsInWarehouse { get; set; } = new List<ProductInWarehouse>();

    }
}
