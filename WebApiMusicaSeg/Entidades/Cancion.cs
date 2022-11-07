using System.ComponentModel.DataAnnotations;
using WebApiMusicaSeg.Validaciones;

namespace WebApiMusicaSeg.Entidades
{
    public class Cancion
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} solo puede tener hasta 30 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<Albums> Albums { get; set; }

        public List<ArtistaCancion> ArtistaCancion { get; set; }
    }
}
