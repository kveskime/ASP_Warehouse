using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class WorkType
    {
        public int WorkTypeId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }
        public virtual List<ProductInWork> ProductsInWork { get; set; } = new List<ProductInWork>();

    }
}