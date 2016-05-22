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

namespace ASPWarehouse.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IPurchaseUOW _purchaseUow;
        private readonly IWarehouseUOW _warehouseUow;


        public ProductsController(IPurchaseUOW purchaseUow, IWarehouseUOW warehouseUow)
        {
            _purchaseUow = purchaseUow;
            _warehouseUow = warehouseUow;
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = _purchaseUow.Products.All;
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _purchaseUow.Products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var vm = new ProductCreateEditViewModel()
            {
                ProductTypeSelectList = new SelectList(_purchaseUow.ProductTypes.All, "ProductTypeId", "Description"),
                WorkMultiSelectList = new MultiSelectList(_warehouseUow.WorkTypes.All,"WorkTypeId","Description"),
                WarehouseMultiSelectList = new MultiSelectList(_warehouseUow.Warehouses.All, "WarehouseId", "Description")
            };
            
            return View(vm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var workId in vm.WorkIds)
                {
                    vm.Product.ProductInWorks.Add(new ProductInWork()
                    {
                        WorkTypeId = workId
                        //ProductId = vm.Product.ProductId
                    });
                }
                _purchaseUow.Products.Add(vm.Product);
                _purchaseUow.Commit();
                return RedirectToAction("Index");
            }
            vm.WorkMultiSelectList = new MultiSelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description");
            vm.ProductTypeSelectList= new SelectList(_purchaseUow.ProductTypes.All, "ProductTypeId", "Description");
            vm.WarehouseMultiSelectList = new MultiSelectList(_warehouseUow.Warehouses.All, "WarehouseId", "Description");

            return View(vm);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _purchaseUow.Products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var vm = new ProductCreateEditViewModel()
            {
                Product = product,
                ProductTypeSelectList = new SelectList(_purchaseUow.ProductTypes.All, "ProductTypeId", "Description", product.ProductTypeId),
                WorkMultiSelectList = new MultiSelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description",
                _warehouseUow.ProductInWorks.All.Where(a => a.ProductId == product.ProductId).Select(b=>b.WorkTypeId).ToArray()),
                WarehouseMultiSelectList = new MultiSelectList(_warehouseUow.Warehouses.All, "WarehouseId", "Description",
                _warehouseUow.ProductInWarehouses.All.Where(a => a.ProductId == product.ProductId).Select(b=> b.WarehouseId).ToArray())
            };
            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProductCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {

                foreach (var productInWorks in _warehouseUow.ProductInWorks.All.Where(a=> a.ProductId == vm.Product.ProductId))
                {
                    _warehouseUow.ProductInWorks.Delete(productInWorks);
                }
                foreach (var productInWarehouses in _warehouseUow.ProductInWarehouses.All.Where(a => a.ProductId == vm.Product.ProductId))
                {
                    _warehouseUow.ProductInWarehouses.Delete(productInWarehouses);
                }
                _warehouseUow.Products.Update(vm.Product);

                _purchaseUow.Commit();
                foreach (var workId in vm.WorkIds)
                {
                    vm.Product.ProductInWorks.Add(new ProductInWork()
                    {
                        WorkTypeId = workId,
                        ProductId = vm.Product.ProductId
                    });
                }
                foreach (var warehouseId in vm.WarehouseIds)
                {
                    vm.Product.ProductInWarehouses.Add(new ProductInWarehouse()
                    {
                        WarehouseId = warehouseId,
                        ProductId = vm.Product.ProductId
                    });
                }

                _purchaseUow.Commit();
                return RedirectToAction("Index");
            }
            vm.ProductTypeSelectList= new SelectList(_purchaseUow.ProductTypes.All, "ProductTypeId", "Description");
            vm.WorkMultiSelectList = new MultiSelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description");
            return View(vm);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _purchaseUow.Products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _purchaseUow.Products.Delete(id);
            _purchaseUow.Commit();
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
