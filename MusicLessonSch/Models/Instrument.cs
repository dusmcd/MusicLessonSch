﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class Instrument
    {
        public int Id { get; set; }

        [Required, DisplayName("Instrument Name")]
        public string? Name { get; set; }

        [DisplayName("Minimum Age")]
        public int MinAge { get; set; }

        public List<Student> Students { get; } = [];

        public List<Teacher> Teachers { get; } = [];
    }
}
