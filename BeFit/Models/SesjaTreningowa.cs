using System.ComponentModel.DataAnnotations;
using System;

namespace BeFit.Models
{
    public class SesjaTreningowa
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data i czas rozpoczęcia sesji", Description = "Data i czas rozpoczęcia sesji")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Data i czas zakończenia sesji", Description = "Data i czas zakończenia sesji")]
        public DateTime EndTime { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<Cwiczenie> Cwiczenies { get; set; }
    }
}
