using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComm.DataAccess;
using EComm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EComm.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository _repository;

        public ProductController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Search(string q)
        {
            var results = _repository.GetProductsByName(q);
            ViewBag.Q = q;

            return View(results);
        }

        public IActionResult Edit(int id)
        {
            var product = _repository.GetProduct(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            // save

            return RedirectToAction("Index", "Home");
        }
    }
}