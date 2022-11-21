using Microsoft.AspNetCore.Identity;

namespace WebApiMusicaSeg.Entidades
{
    public class Albums
    {
        public int Id { get; set; }
        public string Tema { get; set; }

        public int CancionId { get; set; }

        public Cancion Cancion { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }

    }
}
