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
    public class WorkTypesController : Controller
    {
        private readonly IWarehouseUOW _warehouseUow;

        public WorkTypesController(IWarehouseUOW warehouseUow)
        {
            _warehouseUow = warehouseUow;
        }

        // GET: WorkTypes
        public ActionResult Index()
        {
            return View(_warehouseUow.WorkTypes.All);
        }

        // GET: WorkTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = _warehouseUow.WorkTypes.GetById(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // GET: WorkTypes/Create
        public ActionResult Create()
        {
            var vm = new WorkTypeCreateEditViewModel() {ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Name") };
            return View(vm);
        }

        // POST: WorkTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var productId in vm.ProductIds)
                {
                    _warehouseUow.ProductInWorks.Add(new ProductInWork() { ProductId = productId });
                }
                _warehouseUow.WorkTypes.Add(vm.WorkType);
                _warehouseUow.Commit();
                return RedirectToAction("Index");
            }
            vm.ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Name", vm.ProductIds);
            return View(vm);
        }

        // GET: WorkTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = _warehouseUow.WorkTypes.GetById(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            var vm = new WorkTypeCreateEditViewModel()
            {
                ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All, "ProductId", "Name"
                ,_warehouseUow.ProductInWorks.All.Where(a => a.WorkTypeId == workType.WorkTypeId).Select(b=> b.ProductId).ToArray()),
                WorkType = workType
            };//                _repoAuthorBooks.All.Where(a => a.BookId == book.BookId).Select(b => b.AuthorId).ToArray())
            return View(vm);
        }
        


        // POST: WorkTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var productInWork in _warehouseUow.ProductInWorks.All
                    .Where(a => a.WorkTypeId == vm.WorkType.WorkTypeId))
                {
                    _warehouseUow.ProductInWorks.Delete(productInWork);
                }


                _warehouseUow.WorkTypes.Update(vm.WorkType);
                _warehouseUow.Commit();
                foreach (var productId in vm.ProductIds)
                {
                    vm.WorkType.ProductsInWork.Add(new ProductInWork()
                    {
                        ProductId = productId,
                        ProductInWorkId = vm.WorkType.WorkTypeId
                    });
                }
                _warehouseUow.Commit();

                return RedirectToAction("Index");
            }
            vm.ProductsMultiSelectList = new MultiSelectList(_warehouseUow.Products.All,"ProductId", "Name");
            return View(vm);
        }

        // GET: WorkTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = _warehouseUow.WorkTypes.GetById(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // POST: WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkType workType = _warehouseUow.WorkTypes.GetById(id);
            _warehouseUow.WorkTypes.Delete(workType);
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
