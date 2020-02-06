using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RecycleBin.Model.Model;
using RecycleBin.Models;
using RecycleBin.Manager.Manager;

namespace RecycleBin.Controllers
{
    public class ProductController : Controller
    {
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager
                .GetAll()
                .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }
                ).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductViewModel productViewModel)
        {

            string message = "";
            if (ModelState.IsValid)
            {
                Product product = Mapper.Map<Product>(productViewModel);

                if (_productManager.Add(product))
                {
                    message = "Product Saved Successfully";
                }
                else
                {
                    message = "Failed";
                }
            }
            else
            {
                ViewBag.ModelStateMessage = "Model State Failed";
            }

            ViewBag.Messagee = message;
            productViewModel.Products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager
                .GetAll()
                .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }
                ).ToList();
            return View(productViewModel);
        }

        
    }
}