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
    public class PurchasesController : BaseController
    {
        private readonly IPurchaseUOW _purchaseUow;

        public PurchasesController(IPurchaseUOW purchaseUow)
        {
            _purchaseUow = purchaseUow;
        }

        // GET: Purchases
        public ActionResult Index()
        {
            var purchases = _purchaseUow.Purchases.All;
            return View(purchases);
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = _purchaseUow.Purchases.GetById(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            var vm = new PurchaseCreateEditViewModel()
            {
                ProductsMultiSelectList = new MultiSelectList(_purchaseUow.Products.All, "ProductId", "Name"),
                SuppliersSelectList = new SelectList(_purchaseUow.Suppliers.All, "SupplierId", "Name")
            };
            return View(vm);
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var productId in vm.ProductIds)
                {
                    vm.Purchase.CreationDateTime = DateTime.Now;
                    vm.Purchase.ProductsInPurchase.Add(new ProductInPurchase() { ProductId = productId });
                }
                _purchaseUow.Purchases.Add(vm.Purchase);
                _purchaseUow.Commit();

                return RedirectToAction("Index");
            }
            vm.SuppliersSelectList = new SelectList(_purchaseUow.Suppliers.All, "SupplierId", "Name", vm.Purchase.SupplierId);
            return View(vm);
        }

        // GET: Supplier/Create
        public ActionResult SupplierCreate()
        {
            var vm = new SupplierCreateEditViewModel()
            {};
            return View(vm);
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierCreate(SupplierCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _purchaseUow.Suppliers.Add(vm.Supplier);
                _purchaseUow.Commit();

                return RedirectToAction("Create");
            }
            return View(vm);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = _purchaseUow.Purchases.GetById(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            var vm = new PurchaseCreateEditViewModel()
            {
                Purchase = purchase,
                SuppliersSelectList = new SelectList(_purchaseUow.Suppliers.All, "SupplierId", "Name"),
                ProductsMultiSelectList = new SelectList(_purchaseUow.Products.All, "ProductId", "Name",
                _purchaseUow.ProductInPurchases.All.Where(a => a.PurchaseId == purchase.PurchaseId).Select(b => b.ProductId).ToArray())
            };
            vm.SuppliersSelectList = new SelectList(_purchaseUow.Suppliers.All, "SupplierId", "Name", purchase.SupplierId);
            vm.ProductsMultiSelectList = new SelectList(_purchaseUow.Products.All, "ProductId", "Name",
                _purchaseUow.ProductInPurchases.All.Where(a => a.PurchaseId == purchase.PurchaseId)
                    .Select(b => b.ProductId)
                    .ToArray());
            return View(vm);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var productInPurchase in _purchaseUow.ProductInPurchases.All.Where(a => a.PurchaseId == vm.Purchase.PurchaseId))
                {
                    _purchaseUow.ProductInPurchases.Delete(productInPurchase);
                }
                _purchaseUow.Purchases.Update(vm.Purchase);
                _purchaseUow.Commit();
                foreach (var productId in vm.ProductIds)
                {
                    vm.Purchase.ProductsInPurchase.Add(new ProductInPurchase()
                    {
                        ProductId = productId,
                        ProductInPurchaseId = vm.Purchase.PurchaseId
                    });
                }
                _purchaseUow.Commit();
                return RedirectToAction("Index");
            }
            //vm.SuppliersSelectList = new SelectList(_purchaseUow.Suppliers.All, "SupplierId", "Name", vm.Purchase.SupplierId);
            return View(vm);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = _purchaseUow.Purchases.GetById(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = _purchaseUow.Purchases.GetById(id);
            _purchaseUow.Purchases.Delete(purchase);
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
