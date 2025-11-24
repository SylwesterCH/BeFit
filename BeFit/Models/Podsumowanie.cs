using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Podsumowanie
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sesja treningowa", Description = "Numer sesji treningowej")]
        public int SesjaTreningowaId { get; set; }
        public SesjaTreningowa SesjaTreningowa { get; set; }

        [Required]
        [Display(Name = "Ćwiczenie", Description = "Jaki typ ćwiczenia wykonano")]
        public int CwiczenieId { get; set; }
        public Cwiczenie Cwiczenie { get; set;}

        [Required]
        [Display(Name = "Użytkownik", Description = "Osobiste podsumowanie 4 tygodni")]
        public string AppUserId {  get; set; }
        public AppUser AppUser { get; set; }

        [Display(Name = "Obciążenie", Description = "Podaj ile Kg użyto do ćwiczenia")]
        public double Obciazenie { get; set; }

        [Display(Name = "Seria", Description = "Podaj ilość serii")]
        public int Serie { get; set; }

        [Display(Name = "Powtórzenia", Description = "Podaj ilość powtórzeń w serii")]
        public int Powtorzenia { get; set; }
    }
}
