using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.API.Helper;
using Peliculas.BM.ActorBM.Interface;
using Peliculas.DT.DTOs.ActorDTOs;
using Peliculas.DT.DTOs.PaginacionDTO;
using Peliculas.DT.Entidades;

namespace Peliculas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IBMActor bMActor;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public ActorController(IBMActor bMActor, IMapper mapper, ApplicationDbContext context )
        {
            this.bMActor = bMActor;
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO )
        {
            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable,paginacionDTO.CantidadRegistrosPagina);

            var actores = await bMActor.ConsultarActoresAsync(paginacionDTO);
            if (actores is null)
            {
                return Ok(new List<ActorDTO>());
            }
            return Ok(actores);
        }

        [HttpGet("{id:int}",Name ="ConsultarActorById")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await bMActor.ConsultarActorByIdAsync(id);
            if (actor is null)
            {
                return NotFound();
            }
            return actor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {   
            var _actorCreacionDTO = await bMActor.CrearActorAsync(actorCreacionDTO);
            if (_actorCreacionDTO is null) { return BadRequest(); }

            return new CreatedAtRouteResult("ConsultarActorById", new { _actorCreacionDTO.Id }, _actorCreacionDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var resultadoActualizacion = await bMActor.ActualizacionActorAsync(id, actorCreacionDTO);
            
            if (resultadoActualizacion == null) {return NotFound();}
            if (resultadoActualizacion== false) {return BadRequest();}

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var resultado = await bMActor.EliminarActorAsync(id);
            if (resultado is  null){ return NotFound(); }
            if (resultado == null){ return BadRequest(); }

            return NoContent();
        
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ActorPathcDTO> patchDocument  )
        {
            if (patchDocument is null)
            {
                return BadRequest();
            }

            var actorDb = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDb == null)
            {
                return null;
            }


            var actorDTO = mapper.Map<ActorPathcDTO>(actorDb);

            patchDocument.ApplyTo(actorDTO, ModelState);
            var esValido = TryValidateModel(actorDTO);
            if (!esValido) { return BadRequest(ModelState);}

            mapper.Map(actorDTO, actorDb);
            await context.SaveChangesAsync();
            return NoContent();

        }

    }
}
