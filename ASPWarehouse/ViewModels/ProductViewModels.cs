using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Models;

namespace ASPWarehouse.ViewModels
{
    public class ProductTypeCreateEditViewModel
    {
        public ProductType ProductType { get; set; }
        
    }

    public class ProductCreateEditViewModel
    {
        public Product Product { get; set; }
        public SelectList ProductTypeSelectList { get; set; }
        public int[] WorkIds { get; set; }
        public MultiSelectList WorkMultiSelectList { get; set; }
        public int[] WarehouseIds { get; set; }
        public MultiSelectList WarehouseMultiSelectList { get; set; }
        public SelectList TranslatedProductTypes { get; set; }

    }

    public class ProductsWarehouseCreateEditViewModel
    {
        public ProductInWarehouse ProductsInWarehouses { get; set; }
        public int[] ProductIds { get; set; }
        public MultiSelectList Products { get; set; }
        public int[] WarehouseIds { get; set; }
        public MultiSelectList WarehouseMultiSelectList { get; set; }
        

    }

    public class WorkTypeCreateEditViewModel
    {
        public WorkType WorkType { get; set; }
        public int[] ProductIds { get; set; }
        public MultiSelectList ProductsMultiSelectList { get; set; }
        
    }


    public class ProductInWorkCreateEditViewModel
    {
        public ProductInWork ProductInWork { get; set; }
        public int[] ProductIds { get; set; }
        public MultiSelectList ProductsMultiSelectList { get; set; }
        public int[] WorkIds { get; set; }
        public MultiSelectList WorksMultiSelectList { get; set; }

    }
}