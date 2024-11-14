using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicLessonSch.Data;
using MusicLessonSch.Models;

namespace MusicLessonSch.Controllers
{
    public class TeachersController : Controller
    {
        private readonly MusicLessonSchContext _context;

        public TeachersController(MusicLessonSchContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            var teachers = _context.Teacher.Include(t => t.Instruments);
            return View(await teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public async Task<IActionResult> Create()
        {
            var instruments = await _context.Instrument.ToListAsync();
            var teacherVM = new TeacherViewModel
            {
                Instruments = instruments,
                DateOfBirth = DateTime.Now,
            };
            return View(teacherVM);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "Name", "PhoneNumber", "Email", "DateOfBirth", "InstrumentId")] TeacherViewModel teacherVM)
        {
            if (ModelState.IsValid)
            {
                var instrument = _context.Instrument.Where(i => i.Id == teacherVM.InstrumentId).First();
                var teacher = new Teacher
                {
                    Name = teacherVM.Name,
                    PhoneNumber = teacherVM.PhoneNumber,
                    Email = teacherVM.Email,
                    DateOfBirth = teacherVM.DateOfBirth,
                };
                teacher.Instruments.Add(instrument);
                _context.Teacher.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var instruments = await _context.Instrument.ToListAsync();
            foreach(var i in instruments)
            {
                teacherVM.Instruments.Add(i);
            }
            return View(teacherVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Email,DateOfBirth")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher != null)
            {
                _context.Teacher.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddInstrument(int id, string name)
        {
            var instruments = await _context.Instrument
                .Include(i => i.Teachers)
                .Where(i => !i.Teachers.Any(t => t.Id == id))
                .ToListAsync();
            if (instruments.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var teacherVM = new TeacherViewModel
            {
                Id = id,
                Instruments = instruments,
                Name = name
            };
            return View(teacherVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInstrument([Bind("Id","InstrumentId")] TeacherViewModel teacherVM)
        {
            try
            {
                var teacher = _context.Teacher
                    .Where(t => t.Id == teacherVM.Id)
                    .Include(t => t.Instruments)
                    .First();
                var instrument = _context.Instrument
                    .Where(i => i.Id == teacherVM.InstrumentId)
                    .First();
                if (IsDuplicate(teacher, teacherVM.InstrumentId))
                {
                    throw new Exception("Duplicate entry");
                }
                teacher.Instruments.Add(instrument);
                _context.Teacher.Update(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            } catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        private bool IsDuplicate(Teacher teacher, int instrumentId)
        {
            return teacher.Instruments.Any(i => i.Id == instrumentId);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.Id == id);
        }
    }
}
