namespace WebApiMusicaSeg.DTOs
{
    public class CancionDTOConArtistas: CancionDTO
    {
        public List<GetArtistaDTO> Artistas { get; set; }
    }
}
