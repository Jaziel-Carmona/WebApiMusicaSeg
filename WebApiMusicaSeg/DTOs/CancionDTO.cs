namespace WebApiMusicaSeg.DTOs
{
    public class CancionDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<AlbumDTO> Albums { get; set; }
    }
}
