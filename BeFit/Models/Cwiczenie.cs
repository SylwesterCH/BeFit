using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Cwiczenie
    {
        public int Id { get; set; }
        [MaxLength(255)]

        [Display(Name = "Nazwa Ćwiczenia", Description = "Wpisz typ ćwiczenia")]
        public string Name { get; set; }

        public ICollection<Podsumowanie> Podsumowania { get; set; } = new List<Podsumowanie>();
    }
}
