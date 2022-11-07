namespace WebApiMusicaSeg.Entidades
{
    public class ArtistaCancion
    {
        public int ArtistaId { get; set; }
        public int CancionId { get; set; }
        public int Orden_Lanzamiento { get; set; }
        public Artista Artista { get; set; }
        public Cancion Cancion { get; set; }
    }
}
