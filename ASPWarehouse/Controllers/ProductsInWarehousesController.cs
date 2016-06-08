//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ASPWarehouse.ViewModels;
//using DAL;
//using DAL.Interfaces;
//using Domain.Models;

//namespace ASPWarehouse.Controllers
//{
//    public class ProductsInWarehousesController : BaseController
//    {
//        private readonly IPurchaseUOW _purchaseUow;
//        private readonly IWarehouseUOW _warehouseUow;


//        public ProductsInWarehousesController(IPurchaseUOW purchaseUow, IWarehouseUOW warehouseUow)
//        {
//            _purchaseUow = purchaseUow;
//            _warehouseUow = warehouseUow;
//        }

//        // GET: ProductsInWarehouses
//        public ActionResult Index()
//        {
//            return View(_warehouseUow.ProductInWarehouses.All);
//        }

//        // GET: ProductsInWarehouses/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProductInWarehouse productInWarehouse = _warehouseUow.ProductInWarehouses.GetById(id);
//            if (productInWarehouse == null)
//            {
//                return HttpNotFound();
//            }
//            return View(productInWarehouse);
//        }

//        // GET: ProductsInWarehouses/Create
//        public ActionResult Create()
//        {
//            var vm = new ProductsWarehouseCreateEditViewModel()
//            {
//                Products = new MultiSelectList(_purchaseUow.ProductTypes.All, "ProductTypeId", "Description"),
//                WarehouseMultiSelectList = new MultiSelectList(_warehouseUow.Warehouses.All, "WarehouseId", "Description")
//            };
//            return View();
//        }

//        // POST: ProductsInWarehouses/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(ProductsWarehouseCreateEditViewModel vm)
//        {
//            if (ModelState.IsValid)
//            {
//                _warehouseUow.ProductInWarehouses.Add(vm.ProductsInWarehouses);
//                _warehouseUow.Commit();
//                return RedirectToAction("Index");
//            }

//            vm.Products = new MultiSelectList(_purchaseUow.ProductTypes.All, "ProductTypeId", "Description");
//            vm.WarehouseMultiSelectList = new MultiSelectList(_warehouseUow.Warehouses.All, "WarehouseId", "Description");

//            return View(vm);
//        }

//        // GET: ProductsInWarehouses/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProductInWarehouse productInWarehouse = _warehouseUow.ProductInWarehouses.GetById(id);
//            if (productInWarehouse == null)
//            {
//                return HttpNotFound();
//            }
//            var vm = new ProductsWarehouseCreateEditViewModel()
//            {
//                ProductsInWarehouses= productInWarehouse,
//                WarehouseMultiSelectList = new MultiSelectList(_warehouseUow.Warehouses.All, "WarehouseId", "Description",
//                _warehouseUow.ProductInWarehouses.All.Where(a => a.ProductId == productInWarehouse.ProductId).Select(b => b.WarehouseId).ToArray()),
//                Products = new MultiSelectList(_warehouseUow.Products.All,"ProductId", "Description", productInWarehouse.WarehouseId)
//            };
//            return View(productInWarehouse);
//        }

//        // POST: ProductsInWarehouses/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ProductInWarehouseId,Description,WarehouseId,ProductId,CreatedAtDT,CreatedBy,ModifiedAtDT,ModifiedBy")] ProductInWarehouse productInWarehouse)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(productInWarehouse).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productInWarehouse.ProductId);
//            ViewBag.WarehouseId = new SelectList(db.Warehouses, "WarehouseId", "Description", productInWarehouse.WarehouseId);
//            return View(productInWarehouse);
//        }

//        // GET: ProductsInWarehouses/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProductInWarehouse productInWarehouse = db.ProductInWarehouses.Find(id);
//            if (productInWarehouse == null)
//            {
//                return HttpNotFound();
//            }
//            return View(productInWarehouse);
//        }

//        // POST: ProductsInWarehouses/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            ProductInWarehouse productInWarehouse = db.ProductInWarehouses.Find(id);
//            db.ProductInWarehouses.Remove(productInWarehouse);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
