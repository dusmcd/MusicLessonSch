using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public async Task<IActionResult> AddInstrument(int id, string name)
        {
            Instrument[] instruments = await _context.Instrument
                .Include(i => i.Students)
                .Where(i => !i.Students.Any(s => s.Id == id))
                .ToArrayAsync();

            InstrumentViewModel[] viewModels = new InstrumentViewModel[instruments.Length];
            Instrument.MapListVMToModel(instruments, viewModels, new InstrumentViewModel() { });

            StudentViewModel studentVM = new StudentViewModel()
            {
                Id = id,
                Name = name,
                Instruments = viewModels.ToList()
            };

            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInstrument([Bind("Id","InstrumentId")] StudentViewModel studentVM)
        {
            Student student = _context.Student.Find(studentVM.Id)!;
            Instrument instrument = _context.Instrument.Find(studentVM.InstrumentId)!;

            student.Instruments.Add(instrument);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id)
        {
            Student student = await _context.Student
                .Where(s => s.Id == id)
                .Include(s => s.Instruments)
                .FirstAsync();

            int numInst = student.Instruments.Count();
            InstrumentViewModel[] viewModels = new InstrumentViewModel[numInst];
            InstrumentViewModel defaultVM = new InstrumentViewModel() { };
            Instrument.MapListVMToModel(student.Instruments.ToArray(), viewModels, defaultVM);

            StudentViewModel studentVM = new StudentViewModel() { };
            student.MapPropsToVM(studentVM);
            studentVM.Instruments = viewModels.ToList();

            return View(studentVM);
        }
    }
}
