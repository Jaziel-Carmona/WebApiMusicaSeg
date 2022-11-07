namespace WebApiMusicaSeg.DTOs
{
    public class ArtistaDTOConCanciones: GetArtistaDTO
    {
        public List<CancionDTO> Canciones { get; set; }
    }
}
