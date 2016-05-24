using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPWarehouse.ViewModels;
using DAL;
using DAL.Interfaces;
using Domain;
using Domain.Models;

namespace ASPWarehouse.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly IWarehouseUOW _warehouseUow;

        public WarehousesController(IWarehouseUOW warehouseUow)
        {
            _warehouseUow = warehouseUow;
        }

        // GET: Warehouses
        public ActionResult Index()
        {
            return View(_warehouseUow.Warehouses.All);
        }

        // GET: Warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = _warehouseUow.Warehouses.GetById(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            var vm = new WarehouseCreateEditViewModel()
            {
                ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Name")
            };
            return View(vm);
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WarehouseCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var productId in vm.ProductIds)
                {
                    _warehouseUow.ProductInWarehouses.Add(new ProductInWarehouse()
                    {
                        Description = "ProductsInWH",
                        ProductId = productId,
                    });
                }
                _warehouseUow.Warehouses.Add(vm.Warehouse);
                _warehouseUow.Commit();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = _warehouseUow.Warehouses.GetById(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            var vm = new WarehouseCreateEditViewModel()
            {
                Warehouse = warehouse,
                ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Name",
                _warehouseUow.ProductInWarehouses.All.Where(a=> a.WarehouseId == warehouse.WarehouseId).Select(b=>b.ProductId).ToArray())
            };
            return View(vm);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WarehouseCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var productInWarehouse in _warehouseUow.ProductInWarehouses.All.Where(a => a.WarehouseId == vm.Warehouse.WarehouseId))
                {
                    _warehouseUow.ProductInWarehouses.Delete(productInWarehouse);
                }
                _warehouseUow.Warehouses.Update(vm.Warehouse);
                _warehouseUow.Commit();
                foreach (var productId in vm.ProductIds)
                {
                    _warehouseUow.ProductInWarehouses.Add(new ProductInWarehouse()
                    {
                        ProductId = productId,
                        Description = DateTime.Now.ToShortDateString(),
                        WarehouseId = vm.Warehouse.WarehouseId
                    });
                }
                _warehouseUow.Commit();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Warehouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = _warehouseUow.Warehouses.GetById(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Warehouse warehouse = _warehouseUow.Warehouses.GetById(id);
            _warehouseUow.Warehouses.Delete(warehouse);
            _warehouseUow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}

