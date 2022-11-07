using System.ComponentModel.DataAnnotations;
using WebApiMusicaSeg.Validaciones;

namespace WebApiMusicaSeg.DTOs
{
    public class CancionPatchDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
