using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Parts.DAL;
using Project_Parts.Models;

namespace Project_Parts.Controllers
{
    public class CarController : Controller
    {
        public ICarRepositories _carRepo =null;
        public CarController(ICarRepositories carRepo) { this._carRepo = carRepo; }
        public IActionResult Index()
        {
            var data = this._carRepo.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Car c)
        {
            if (ModelState.IsValid)
            {
                if (this._carRepo.Insert(c))
                {
                    return RedirectToAction("Create","Parts", new { id=c.CarId});
                }
                else
                {
                    NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid insert.");
            }
            
            return View();
        }
        public IActionResult Edit(int id)
        {
            var data = this._carRepo.Get(id);
            if (data == null)
                return NotFound();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit([FromBody]Car car)
        {
            if (ModelState.IsValid)
            {
                this._carRepo.Update(car);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult Delete(int id)
        {
            var data = this._carRepo.GetById(id);
            return View(data);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                if (this._carRepo.Delete(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            ModelState.AddModelError("", "Invalid data");
            return View();
        }


    }
}