using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Parts.DAL;
using Project_Parts.Models;

namespace Project_Parts.Controllers
{
    public class PartsController : Controller
    {
        IPartsRepositories repo = null;
        public PartsController(IPartsRepositories repo) { this.repo = repo; }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int id)
        {
            ViewBag.CarId = id;
            return View();
        }
        [HttpPost]
        public JsonResult CreatePost([FromBody]CarParts[] parts)
        {
            if (ModelState.IsValid)
            {
                this.repo.Insert(parts);
                return Json(new { success = true, message = "Data save succeeded" });
            }
            return Json(new { success = false, message = "Data save failed" });
        }
    }
}