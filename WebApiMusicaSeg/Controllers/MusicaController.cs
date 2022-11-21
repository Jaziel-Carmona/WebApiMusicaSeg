using WebApiMusicaSeg.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusicaSeg.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApiMusicaSeg.Controllers
{
    [ApiController]
    [Route("artistas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class MusicaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;


        public MusicaController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetArtistaDTO>>> Get()
        {
            var artistas = await dbContext.Artistas.ToListAsync();
            return mapper.Map<List<GetArtistaDTO>>(artistas);
        }

        [HttpGet("{id:int}", Name = "obtenerartista")]
        public async Task<ActionResult<ArtistaDTOConCanciones>> Get(int id)
        {
            var artista = await dbContext.Artistas
                .Include(artistaDB => artistaDB.ArtistaCancion)
                .ThenInclude(artistaCancionDB => artistaCancionDB.Cancion)
                .FirstOrDefaultAsync(artistaBD => artistaBD.Id == id);

            if (artista == null)
            {
                return NotFound();
            }

            return mapper.Map<ArtistaDTOConCanciones>(artista);

        }

        [HttpGet("obtenerArtista/{nombre}")]
        public async Task<ActionResult<List<GetArtistaDTO>>> Get([FromRoute] string nombre)
        {
            var artista = await dbContext.Artistas.Where(artistaBD => artistaBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetArtistaDTO>>(artista);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ArtistaDTO artistaDto)
        {

            var existeArtistaMismoNombre = await dbContext.Artistas.AnyAsync(x => x.Nombre == artistaDto.Nombre);

            if (existeArtistaMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {artistaDto.Nombre}");
            }

            var artista = mapper.Map<Artista>(artistaDto);

            dbContext.Add(artista);
            await dbContext.SaveChangesAsync();

            var artistaDTO = mapper.Map<GetArtistaDTO>(artista);

            return CreatedAtRoute("obtenerartista", new { id = artista.Id }, artistaDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ArtistaDTO artistaCreacionDTO, int id)
        {
            var exist = await dbContext.Artistas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var artista = mapper.Map<Artista>(artistaCreacionDTO);
            artista.Id = id;

            dbContext.Update(artista);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Artistas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Artista()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
