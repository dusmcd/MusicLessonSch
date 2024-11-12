using Microsoft.Identity.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class Availability
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

        public Day Day { get; set; }

        [Required, DisplayName("Start Time"), DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required, DisplayName("End Tiime"), DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
    }
}
