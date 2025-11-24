using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Cwiczenie
    {
        public int Id { get; set; }
        [MaxLength(255)]

        [Display(Name = "Nazwa Ćwiczenia", Description = "Wpisz typ ćwiczenia")]
        public string Name { get; set; }

        public int SesjaTreningowaId { get; set; }
        public SesjaTreningowa SesjaTreningowa { get; set; }
    }
}
