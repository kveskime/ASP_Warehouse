using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Models;

namespace ASPWarehouse.ViewModels
{
    public class WarehouseCreateEditViewModel
    {
        public Warehouse Warehouse { get; set; }
        public int[] ProductIds { get; set; }
        public MultiSelectList ProductsMultiSelectList { get; set; }

    }
}