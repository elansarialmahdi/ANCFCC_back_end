namespace Mohafadati.Services.Titres.Models.Dto
{
    public class TitreDto
    {
        public required string ConservationFonciere { get; set; }
        public required int NumeroTitre { get; set; }
        public required string Indice { get; set; }
        public string? IndiceSpecial { get; set; }
    }
}
