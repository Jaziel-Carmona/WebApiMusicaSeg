using System.ComponentModel.DataAnnotations;

namespace WebApiMusicaSeg.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
