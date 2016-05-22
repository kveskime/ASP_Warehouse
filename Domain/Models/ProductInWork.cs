using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductInWork
    {
        public int ProductInWorkId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int WorkTypeId { get; set; }
        public virtual WorkType WorkType { get; set; }


    }
}
