using Microsoft.Identity.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class Availability : Model
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

        public Day Day { get; set; }

        [Required, DisplayName("Start Time"), DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required, DisplayName("End Time"), DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        public override Model Copy()
        {
            Availability availability = new Availability();
            availability.Id = Id;
            availability.Day = Day;
            availability.StartTime = StartTime;
            availability.EndTime = EndTime;
            availability.TeacherId = TeacherId;

            return availability;
        }
    }
}
