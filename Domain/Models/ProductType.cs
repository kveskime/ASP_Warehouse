using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}
