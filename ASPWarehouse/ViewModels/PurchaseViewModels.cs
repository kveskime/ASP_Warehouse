using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace ASPWarehouse.ViewModels
{
    public class PurchaseCreateEditViewModel
    {
        public Purchase Purchase { get; set; }
        public int[] ProductIds { get; set; }
        public MultiSelectList ProductsMultiSelectList { get; set; }
        public SelectList SuppliersSelectList { get; set; }
    }

    public class SupplierCreateEditViewModel
    {
        public Supplier Supplier { get; set; }

    }
}