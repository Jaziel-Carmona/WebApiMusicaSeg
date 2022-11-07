using AutoMapper;
using WebApiMusicaSeg.DTOs;
using WebApiMusicaSeg.Entidades;

namespace WebApiMusicaSeg.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ArtistaDTO, Artista>();
            CreateMap<Artista, GetArtistaDTO>();
            CreateMap<Artista, ArtistaDTOConCanciones>()
                .ForMember(artistaDTO => artistaDTO.Canciones, opciones => opciones.MapFrom(MapArtistaDTOCanciones));
            CreateMap<CancionCreacionDTO, Cancion>()
                .ForMember(cancion => cancion.ArtistaCancion, opciones => opciones.MapFrom(MapArtistaCancion));
            CreateMap<Cancion, CancionDTO>();
            CreateMap<Cancion, CancionDTOConArtistas>()
                .ForMember(cancionDTO => cancionDTO.Artistas, opciones => opciones.MapFrom(MapCancionDTOArtistas));
            CreateMap<CancionPatchDTO, Cancion>().ReverseMap();
            CreateMap<AlbumCreacionDTO, Albums>();
            CreateMap<Albums, AlbumDTO>();
        }

        private List<CancionDTO> MapArtistaDTOCanciones(Artista artista, GetArtistaDTO getArtistaDTO)
        {
            var result = new List<CancionDTO>();

            if (artista.ArtistaCancion == null) { return result; }

            foreach (var artistaCancion in artista.ArtistaCancion)
            {
                result.Add(new CancionDTO()
                {
                    Id = artistaCancion.CancionId,
                    Nombre = artistaCancion.Cancion.Nombre
                });
            }

            return result;
        }

        private List<GetArtistaDTO> MapCancionDTOArtistas(Cancion cancion, CancionDTO cancionDTO)
        {
            var result = new List<GetArtistaDTO>();

            if (cancion.ArtistaCancion == null)
            {
                return result;
            }

            foreach (var artistacancion in cancion.ArtistaCancion)
            {
                result.Add(new GetArtistaDTO()
                {
                    Id = artistacancion.ArtistaId,
                    Nombre = artistacancion.Artista.Nombre
                });
            }

            return result;
        }

        private List<ArtistaCancion> MapArtistaCancion(CancionCreacionDTO cancionCreacionDTO, Cancion cancion)
        {
            var resultado = new List<ArtistaCancion>();

            if (cancionCreacionDTO.ArtistasIds == null) { return resultado; }
            foreach (var artistaId in cancionCreacionDTO.ArtistasIds)
            {
                resultado.Add(new ArtistaCancion() { ArtistaId = artistaId });
            }
            return resultado;
        }
    }
}
