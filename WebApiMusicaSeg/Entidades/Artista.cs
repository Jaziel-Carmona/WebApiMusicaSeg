using System.ComponentModel.DataAnnotations;
using WebApiMusicaSeg.Validaciones;

namespace WebApiMusicaSeg.Entidades
{
    public class Artista
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] 
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} solo puede tener hasta 30 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public List<ArtistaCancion> ArtistaCancion { get; set; }
    }
}
