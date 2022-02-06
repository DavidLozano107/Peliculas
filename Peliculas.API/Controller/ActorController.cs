using Microsoft.AspNetCore.Mvc;
using Peliculas.BM.ActorBM.Interface;
using Peliculas.DT.DTOs.ActorDTOs;
using Peliculas.DT.Entidades;

namespace Peliculas.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IBMActor bMActor;

        public ActorController(IBMActor bMActor)
        {
            this.bMActor = bMActor;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var actores = await bMActor.ConsultarActoresAsync();
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
    }
}
