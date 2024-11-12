using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class Lesson
    {
        public int StudentId { get; set; }

        public int TeacherId { get; set; }

        [Required]
        public Day Day { get; set; }

        [Required, DisplayName("Lesson Time"), DataType(DataType.Time)]
        public DateTime LessonTime { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required, DisplayName("First Lesson Date"), DataType(DataType.Date)]
        public DateTime FirstLessonDate { get; set; }



    }

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }
}
