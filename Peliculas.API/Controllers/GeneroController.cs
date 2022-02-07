using Microsoft.AspNetCore.Mvc;
using Peliculas.BM.GeneroBM.Interface;
using Peliculas.DT.DTOs.GeneroDTOs;

namespace Peliculas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        public IBMGenero BMGenero { get; }

        public GeneroController(IBMGenero bMGenero)
        {
            this.BMGenero = bMGenero;
        }


        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var generosListaDTO = await BMGenero.ConsultarGenerosAsync();
            if (generosListaDTO is null)
            {
                return new List<GeneroDTO>();
            }
            return generosListaDTO;
        }

        [HttpGet("{id:int}", Name = "ConsultarGeneroById")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var generoDTO = await BMGenero.ConsultarGeneroByIdAsync(id);

            if (generoDTO is null)
            {
                return NotFound();
            }
            return generoDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var generoDTO = await BMGenero.CrearGeneroAsync(generoCreacionDTO);

            if (generoDTO is null)
            {
                return BadRequest();
            }

            return new CreatedAtRouteResult("ConsultarGeneroById", new { id = generoDTO.Id }, generoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var actualizacionExitosa = await BMGenero.ActualizacionGeneroAsync(id, generoCreacionDTO);

            if (!actualizacionExitosa)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool? eliminacionExiotsa = await BMGenero.EliminarGeneroAsync(id);

            if (eliminacionExiotsa is null) {return BadRequest();}
            if (eliminacionExiotsa == false) {return NotFound();}

            return NoContent();

        }

    }
}
