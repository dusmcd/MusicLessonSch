using Microsoft.AspNetCore.Mvc;
using MusicLessonSch.Models;
using MusicLessonSch.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicLessonSch.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly MusicLessonSchContext _context;

        public InstrumentsController(MusicLessonSchContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var instruments = await _context.Instrument.ToListAsync();
            return View(instruments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "Name", "MinAge")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _context.Instrument.Add(instrument);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var instrument = await _context.Instrument.Where(i => i.Id == id).FirstAsync();
            if (instrument == null)
            {
                return NotFound();
            }
            return View(instrument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id", "Name", "MinAge")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _context.Instrument.Update(instrument);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var instrument = await _context.Instrument.Where(i => i.Id == id).FirstAsync();
            if (instrument == null)
            {
                return NotFound();
            }
            return View(instrument);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var instrument = _context.Instrument.Where(i => i.Id == id).First();
            _context.Instrument.Remove(instrument);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
