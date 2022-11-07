using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using WebApiMusicaSeg.Entidades;
using WebApiMusicaSeg.DTOs;

namespace WebApiMusicaSeg.Controllers
{
    [ApiController]
    [Route("api/canciones")]
    public class CancionesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public CancionesController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cancion>>> GetAll()
        {
            return await dbContext.Canciones.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerCancion")]
        public async Task<ActionResult<CancionDTOConArtistas>> GetById(int id)
        {
            var cancion = await dbContext.Canciones
                .Include(cancionDB => cancionDB.ArtistaCancion)
                .ThenInclude(artistaCancionDB => artistaCancionDB.Artista)
                .Include(albumDB => albumDB.Albums)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cancion == null)
            {
                return NotFound();
            }

            cancion.ArtistaCancion = cancion.ArtistaCancion.OrderBy(x => x.Orden_Lanzamiento).ToList();

            return mapper.Map<CancionDTOConArtistas>(cancion);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CancionCreacionDTO cancionCreacionDTO)
        {

            if (cancionCreacionDTO.ArtistasIds == null)
            {
                return BadRequest("No se puede crear una cancion sin artistas.");
            }

            var artistasIds = await dbContext.Artistas
                .Where(artistaBD => cancionCreacionDTO.ArtistasIds.Contains(artistaBD.Id)).Select(x => x.Id).ToListAsync();

            if (cancionCreacionDTO.ArtistasIds.Count != artistasIds.Count)
            {
                return BadRequest("No existe uno de los artistas enviados");
            }

            var cancion = mapper.Map<Cancion>(cancionCreacionDTO);

            OrdenarPorArtistas(cancion);

            dbContext.Add(cancion);
            await dbContext.SaveChangesAsync();

            var cancionDTO = mapper.Map<CancionDTO>(cancion);

            return CreatedAtRoute("obtenerCancion", new { id = cancion.Id }, cancionDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CancionCreacionDTO cancionCreacionDTO)
        {
            var cancionDB = await dbContext.Canciones
                .Include(x => x.ArtistaCancion)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cancionDB == null)
            {
                return NotFound();
            }

            cancionDB = mapper.Map(cancionCreacionDTO, cancionDB);

            OrdenarPorArtistas(cancionDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Canciones.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            //var validateRelation = await dbContext.ArtistaCancion.AnyAsync


            dbContext.Remove(new Cancion { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        private void OrdenarPorArtistas(Cancion cancion)
        {
            if (cancion.ArtistaCancion != null)
            {
                for (int i = 0; i < cancion.ArtistaCancion.Count; i++)
                {
                    cancion.ArtistaCancion[i].Orden_Lanzamiento = i;
                }
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<CancionPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var cancionDB = await dbContext.Canciones.FirstOrDefaultAsync(x => x.Id == id);

            if (cancionDB == null) { return NotFound(); }

            var cancionDTO = mapper.Map<CancionPatchDTO>(cancionDB);

            patchDocument.ApplyTo(cancionDTO);

            var isValid = TryValidateModel(cancionDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(cancionDTO, cancionDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}

