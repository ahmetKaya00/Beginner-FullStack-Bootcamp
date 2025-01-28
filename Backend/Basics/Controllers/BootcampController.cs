using Basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers{

    public class BootcampController : Controller{


        public IActionResult Index(){
            
            return View(Repository.Bootcamps);
        }
        public IActionResult Details(int? id){
            if(id == null){
                return RedirectToAction("List","Bootcamp");
            }
            var bootcamp = Repository.GetById(id);
            return View(bootcamp);
        }
        public IActionResult List(){

            return View(Repository.Bootcamps);
        }
    }
}