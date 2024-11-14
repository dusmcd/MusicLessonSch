using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required, DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required, DisplayName("Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public List<Student> Students { get; } = [];

        public List<Instrument> Instruments { get; } = [];

        public List<Availability> Availability { get; } = [];
    }
}
