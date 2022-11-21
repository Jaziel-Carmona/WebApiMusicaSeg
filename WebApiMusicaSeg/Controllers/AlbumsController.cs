using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using WebApiMusicaSeg.DTOs;
using WebApiMusicaSeg.Entidades;

namespace WebApiMusicaSeg.Controllers
{
    [ApiController]
    [Route("canciones/{cancionId:int}/albums")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlbumsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public AlbumsController(ApplicationDbContext dbContext, IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlbumDTO>>> Get(int cancionId)
        {
            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);
            if (!existeCancion)
            {
                return NotFound();
            }

            var albums = await dbContext.Albums.Where(albumDB => albumDB.CancionId == cancionId).ToListAsync();

            return mapper.Map<List<AlbumDTO>>(albums);
        }

        [HttpGet("{id:int}", Name = "obtenerAlbum")]
        public async Task<ActionResult<AlbumDTO>> GetById(int id)
        {
            var album = await dbContext.Albums.FirstOrDefaultAsync(albumDB => albumDB.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return mapper.Map<AlbumDTO>(album);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(int cancionId, AlbumCreacionDTO albumCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);
            if (!existeCancion)
            {
                return NotFound();
            }

            var album = mapper.Map<Albums>(albumCreacionDTO);
            album.CancionId = cancionId;
            album.UsuarioId = usuarioId;
            dbContext.Add(album);
            await dbContext.SaveChangesAsync();

            var albumDTO = mapper.Map<AlbumDTO>(album);

            return CreatedAtRoute("obtenerAlbum", new { id = album.Id, cancionId = cancionId }, albumDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int cancionId, int id, AlbumCreacionDTO albumCreacionDTO)
        {
            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);
            if (!existeCancion)
            {
                return NotFound();
            }

            var existeAlbum = await dbContext.Albums.AnyAsync(albumDB => albumDB.Id == id);
            if (!existeAlbum)
            {
                return NotFound();
            }

            var album = mapper.Map<Albums>(albumCreacionDTO);
            album.Id = id;
            album.CancionId = cancionId;

            dbContext.Update(album);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
