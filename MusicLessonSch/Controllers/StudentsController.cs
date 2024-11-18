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
            List<Student> students = await _context.Student.Include(s => s.Instruments).ToListAsync();
            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            Instrument[] instruments = await _context.Instrument.ToArrayAsync();
            InstrumentViewModel[] viewModels = new InstrumentViewModel[instruments.Length];

            Instrument.MapListVMToModel(instruments, viewModels, new InstrumentViewModel() { });
            
            StudentViewModel student = new StudentViewModel
            {
                Instruments = viewModels.ToList()
            };
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","Name","PhoneNumber","Email","Age", "InstrumentId")] StudentViewModel studentVM)
        {
            Student student = new Student { };
            studentVM.MapPropsToModel(student);
            var instrument = _context.Instrument.Find(studentVM.InstrumentId);
            student.Instruments.Add(instrument!);
            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
