using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.API.Helper;
using Peliculas.BM.PeliculaBM.Interface;
using Peliculas.DT.DTOs.PaginacionDTO;
using Peliculas.DT.DTOs.PeliculaDTOs;

namespace Peliculas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IBMPelicula bMPelicula;
        private readonly IMapper mapper;

        public PeliculaController(ApplicationDbContext applicationDbContext, IBMPelicula bMPelicula, IMapper mapper)
        {
            this.context = applicationDbContext;
            this.bMPelicula = bMPelicula;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var respuesta = await bMPelicula.ConsultarPeliculasAsync(paginacionDTO);
            await HttpContext.InsertarParametrosPaginacion(respuesta.queryable, paginacionDTO.CantidadRegistrosPagina);

            return Ok(respuesta.ListaResponse);
        }


        [HttpGet("{id:int}", Name = "ConsultarPeliculaById")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await bMPelicula.ConsultarPeliculaByIdAsync(id);
            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDTO = await bMPelicula.CrearPeliculaAsync(peliculaCreacionDTO);
            if (peliculaDTO is null) { return BadRequest(); }

            return new CreatedAtRouteResult("ConsultarPeliculaById", new { id = peliculaDTO.Id }, peliculaDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDTO = await bMPelicula.ActualizacionPeliculaAsync(id, peliculaCreacionDTO);
            if (peliculaDTO is null) { return NotFound(); }
            if (peliculaDTO == false) { return BadRequest(); }

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Pathc([FromRoute] int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            if (patchDocument is null) { return BadRequest(); }

            var peliculaDb = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (peliculaDb is null) { return NotFound(); }

            var peliculaDTO = mapper.Map<PeliculaPatchDTO>(peliculaDb);
            patchDocument.ApplyTo(peliculaDTO, ModelState);


            var esValido = TryValidateModel(peliculaDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(peliculaDTO, peliculaDb);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var resultado = await bMPelicula.EliminarPeliculaAsync(id);
            if (resultado is null)
            {
                return NotFound();
            }

            if (resultado == false)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
