using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class Student : Model
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

        public List<Instrument> Instruments { get; } = [];

        public override Model Copy()
        {
            Student student = new Student();
            student.Id = Id;
            student.Name = Name;
            student.PhoneNumber = PhoneNumber;
            student.Email = Email;
            student.Age = Age;

            return student;
        }


    }
}
