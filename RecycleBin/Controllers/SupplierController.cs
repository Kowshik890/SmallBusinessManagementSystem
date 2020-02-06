using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RecycleBin.Manager.Manager;
using RecycleBin.Model.Model;
using RecycleBin.Models;

namespace RecycleBin.Controllers
{
    public class SupplierController : Controller
    {
        SupplierManager _supplierManager = new SupplierManager();

        [HttpGet]
        public ActionResult Add()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult Add(SupplierViewModel supplierViewModel)
        {
            Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);

            string message = "";
            if (ModelState.IsValid)
            {
                if (_supplierManager.Add(supplier))
                {
                    message = "Supplier Saved Successfully";
                }
                else
                {
                    message = "Failed";
                }
            }
            else
            {
                ViewBag.ModelSateMessage = "Model State Failed";
            }

            ViewBag.Message = message;
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }
    }
}