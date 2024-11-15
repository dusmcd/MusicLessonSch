using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required, DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required]
        public int Age { get; set; }

        public List<Teacher> Teachers { get; } = [];

        public List<InstrumentViewModel> Instruments { get; set; } = [];

        [Required, DisplayName("Instrument")]
        public int InstrumentId { get; set; }

        [DisplayName("Instrument Name")]
        public string? InstrumentName { get; set; }
    }
}
