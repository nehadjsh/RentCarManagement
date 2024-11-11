using Microsoft.AspNetCore.Mvc;
using RentCarManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentCarManagement.Controllers
{
    public class CarsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult NEw()
        {
            ViewData["CarDropDownList"] =  _context.Rentals.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Car newCar)
        {
           if(ModelState.IsValid)
            {
                _context.Cars.Add(newCar);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CarDropDownList"] = _context.Rentals.ToList();
            return View("New", newCar);
        }
        public IActionResult Index()
        {

            List<Car> carListModel = _context.Cars.Include(d=>d.Rental).ToList();
            return View(carListModel);
        }


        public IActionResult Edit(int id)
        {
            Car? carModle = _context.Cars.FirstOrDefault(e => e.Id == id);
            ViewData["rentList"] = _context.Rentals.ToList();
            return View(carModle);
        }

        
        [HttpPost]
        public IActionResult saveEdit(int id, Car newCar)
        {
            if(ModelState.IsValid)
            {

                Car? oldCar = _context.Cars.FirstOrDefault(e => e.Id == id);

                if (oldCar != null)
                {
                    oldCar.Brand = newCar.Brand;
                    oldCar.Model = newCar.Model;
                    oldCar.DailyRate = newCar.DailyRate;
                    oldCar.IsAvailable = newCar.IsAvailable;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewData["rentList"] = _context.Rentals.ToList();
            return View("Edit", newCar);
        }

    }
}

