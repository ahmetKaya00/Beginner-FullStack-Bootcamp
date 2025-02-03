using System.Threading.Tasks;
using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers{

    public class BootcampController : Controller{

        private readonly DataContext _context;

        public BootcampController(DataContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Bootcamps.Include(k=>k.Ogretmen).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create(){

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bootcamp model){

            _context.Bootcamps.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id){
            if(id == null){
                return NotFound();
            }
            var ogr = await _context.Bootcamps.Include(b=>b.KursKayitlari).ThenInclude(b=>b.Ogrenci).Select(b=> new BootcampViewModel{
                BootcampId = b.BootcampId,
                Baslik = b.Baslik,
                OgretmenId = b.OgretmenId,
                KursKayitlari = b.KursKayitlari
            }).FirstOrDefaultAsync(b=>b.BootcampId == id);

            if(ogr == null){
                return NotFound();
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, BootcampViewModel model){
            if(id != model.BootcampId){
                return NotFound();
            }

            if(ModelState.IsValid){
                try
                {
                    _context.Update(new Bootcamp() {BootcampId = model.BootcampId, Baslik = model.Baslik, OgretmenId = model.OgretmenId});
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Bootcamps.Any(o=>o.BootcampId == model.BootcampId)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int? id){
            if(id == null){
                return NotFound();
            }

            var Bootcamp = await _context.Bootcamps.FindAsync(id);

            if(Bootcamp == null){
                return NotFound();
            }

            return View(Bootcamp);
        }

        [HttpPost]
        public async Task<IActionResult>Delete([FromForm]int id){
            var Bootcamp = await _context.Bootcamps.FindAsync(id);
            if(Bootcamp == null){
                return NotFound();
            }
            _context.Bootcamps.Remove(Bootcamp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}