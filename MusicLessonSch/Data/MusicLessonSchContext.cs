using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLessonSch.Models;

namespace MusicLessonSch.Data
{
    public class MusicLessonSchContext : DbContext
    {
        public MusicLessonSchContext (DbContextOptions<MusicLessonSchContext> options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teacher { get; set; } = default!;

        public DbSet<Student> Student { get; set; } = default!;

        public DbSet<Lesson> Lessons { get; set; } = default!;

        public DbSet<Instrument> Instrument { get; set; } = default!;

        public DbSet<Availability> Availability { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Teachers)
                .UsingEntity<Lesson>();

        }

    }
}
