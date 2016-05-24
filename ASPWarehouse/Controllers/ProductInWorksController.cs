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
    public class ProductInWorksController : Controller
    {
        private readonly IWarehouseUOW _warehouseUow;

        public ProductInWorksController(IWarehouseUOW warehouseUow)
        {
            _warehouseUow = warehouseUow;
        }
        // GET: ProductInWorks
        public ActionResult Index()
        {
            var productInWorks = _warehouseUow.ProductInWorks.All;
            return View(productInWorks);
        }

        // GET: ProductInWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInWork productInWork = _warehouseUow.ProductInWorks.GetById(id);
            if (productInWork == null)
            {
                return HttpNotFound();
            }
            return View(productInWork);
        }

        // GET: ProductInWorks/Create
        public ActionResult Create()
        {
            var vm = new ProductInWorkCreateEditViewModel()
            {
                ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Name"),
                WorksMultiSelectList = new MultiSelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description")
            };
            vm.ProductsMultiSelectList = new SelectList(_warehouseUow.Products.All, "ProductId", "Description");
            vm.WorksMultiSelectList = new SelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description");
            return View(vm);
        }

        // POST: ProductInWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductInWorkCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {

                _warehouseUow.ProductInWorks.Add(vm.ProductInWork);
                _warehouseUow.Commit();
                return RedirectToAction("Index");
            }

            //vm.ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Description", vm.ProductInWork.ProductId);
            //vm.WorksMultiSelectList= new MultiSelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description", vm.ProductInWork.WorkTypeId);
            return View(vm.ProductInWork);
        }

        // GET: ProductInWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInWork productInWork = _warehouseUow.ProductInWorks.GetById(id);
            if (productInWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_warehouseUow.Products.All, "ProductId", "Description", productInWork.ProductId);
            ViewBag.WorkTypeId = new SelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description", productInWork.WorkTypeId);
            return View(productInWork);
        }

        // POST: ProductInWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductInWork productInWork)
        {
            if (ModelState.IsValid)
            {
                _warehouseUow.ProductInWorks.Update(productInWork);
                _warehouseUow.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_warehouseUow.Products.All, "ProductId", "Description", productInWork.ProductId);
            ViewBag.WorkTypeId = new SelectList(_warehouseUow.WorkTypes.All, "WorkTypeId", "Description", productInWork.WorkTypeId);
            return View(productInWork);
        }

        // GET: ProductInWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInWork productInWork = _warehouseUow.ProductInWorks.GetById(id);
            if (productInWork == null)
            {
                return HttpNotFound();
            }
            return View(productInWork);
        }

        // POST: ProductInWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductInWork productInWork = _warehouseUow.ProductInWorks.GetById(id);
            _warehouseUow.ProductInWorks.Delete(productInWork);
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
