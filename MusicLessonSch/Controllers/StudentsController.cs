using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLessonSch.Data;
using MusicLessonSch.Models;

namespace MusicLessonSch.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MusicLessonSchContext _context;

        public StudentsController(MusicLessonSchContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Student.Include(s => s.Instruments).ToListAsync();
            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            var instruments = await _context.Instrument.ToListAsync();
            List<InstrumentViewModel> instrumentsVM = new List<InstrumentViewModel>();
            foreach(var item in instruments)
            {
                var instrumentVM = new InstrumentViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                instrumentsVM.Add(instrumentVM);
            }
            var student = new StudentViewModel
            {
                Instruments = instrumentsVM,
            };
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","Name","PhoneNumber","Email","Age", "InstrumentId")] StudentViewModel studentVM)
        {
            var student = new Student
            {
                Name = studentVM.Name,
                PhoneNumber = studentVM.PhoneNumber,
                Email = studentVM.Email,
                Age = studentVM.Age,
            };
            var instrument = _context.Instrument.Find(studentVM.InstrumentId);
            student.Instruments.Add(instrument!);
            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
