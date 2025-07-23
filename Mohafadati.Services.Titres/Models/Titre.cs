using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Mohafadati.Services.Titres.Models
{
    public class Titre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ConservationFonciere { get; set; }
        [Required]
        public int NumeroTitre { get; set; }
        [Required]
        public string Indice { get; set; }
        public string? IndiceSpecial { get; set; }
    }
}
