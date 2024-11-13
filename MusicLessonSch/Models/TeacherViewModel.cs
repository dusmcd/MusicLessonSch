using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Required, DisplayName("Teacher Name")]
        public string? Name { get; set; }

        [Required, DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required, DisplayName("Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public List<Instrument> Instruments { get; set; } = [];

        [DisplayName("Instrument")]
        public int InstrumentId { get; set; }

        [DisplayName("Instrument Name")]
        public string? InstrumentName { get; set; }
    }
}
