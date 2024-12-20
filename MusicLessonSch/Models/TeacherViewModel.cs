﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class TeacherViewModel : ViewModel
    {
        public int Id { get; set; }

        [Required, DisplayName("Teacher Name")]
        public string? Name { get; set; }

        [Required, DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required, DisplayName("Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public List<InstrumentViewModel> Instruments { get; set; } = [];

        [Required, DisplayName("Instrument")]
        public int InstrumentId { get; set; }

        [DisplayName("Instrument Name")]
        public string? InstrumentName { get; set; }

        public override ViewModel Copy()
        {
            TeacherViewModel teacher = new TeacherViewModel();
            teacher.Id = Id;
            teacher.Name = Name;
            teacher.PhoneNumber = PhoneNumber;
            teacher.DateOfBirth = DateOfBirth;
            teacher.InstrumentId = InstrumentId;
            teacher.InstrumentName = InstrumentName;

            return teacher;
        }
    }
}
