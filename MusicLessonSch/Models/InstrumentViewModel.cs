using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLessonSch.Models
{
    public class InstrumentViewModel : ViewModel
    {
        public int Id { get; set; }

        [Required, DisplayName("Instrument Name")]
        public string? Name { get; set; }

        [DisplayName("Minimum Age")]
        public int MinAge { get; set; }

        public bool IsRemoved { get; set; }

        public override ViewModel Copy()
        {
            InstrumentViewModel viewModel = new InstrumentViewModel();
            viewModel.Id = Id;
            viewModel.Name = Name;
            viewModel.MinAge = MinAge;

            return viewModel;
        }
    }
}
