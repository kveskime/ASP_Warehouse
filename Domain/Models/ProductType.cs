using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ProductType : BaseEntity
    {
        public int ProductTypeId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        [ForeignKey("ProductTypeDescription")]
        [Display(Name = "ProductTypeDescription", ResourceType = typeof(Resources.Domain))]
        public int ProductTypeDescriptionId { get; set; }
        public virtual MultiLangString ProductTypeDescription { get; set; }
        

        public virtual List<Product> Products { get; set; }


        public override string ToString()
        {
            return ProductTypeDescription.ToString();
        }
    }
}
