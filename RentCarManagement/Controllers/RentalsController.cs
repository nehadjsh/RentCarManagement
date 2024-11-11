using Microsoft.AspNetCore.Mvc;
using RentCarManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace RentCarManagement.Controllers
{
    public class RentalsController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            

            //Get All Rental
            List<Rental> rentalModel = _context.Rentals.Include(d => d.Car).ToList();
            return View(rentalModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View(new Rental());
        }
        [HttpPost]
        public IActionResult SaveNEw(Rental rent)
        {
            if(rent.Car != null)
            {
                _context.Rentals.Add(rent);
                _context.SaveChanges();
                return RedirectToAction("Index", "rentals");
            }
            else
            {
                return View("New", rent);
            }

            
        }
    }

}


