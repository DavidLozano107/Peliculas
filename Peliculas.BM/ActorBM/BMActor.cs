using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.BM.ActorBM.Interface;
using Peliculas.BM.Helper.AlmacenarArchivos.Interfaces;
using Peliculas.DT.DTOs.ActorDTOs;
using Peliculas.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.ActorBM
{
    public class BMActor : IBMActor
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public BMActor(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        public async Task<bool?> ActualizacionActorAsync(int id, ActorCreacionDTO actorCreacionDTO)
        {
            //var actor = mapper.Map<Actor>(actorCreacionDTO);
            //actor.Id = id;
            //context.Entry(actor).State = EntityState.Modified;


            var actorDb = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDb == null) { return null; }

            actorDb = mapper.Map(actorCreacionDTO,actorDb);


            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actorDb.Foto = await almacenadorArchivos.EditarArchivoAsync(contenido,extension,contenedor,actorDb.Foto,actorCreacionDTO.Foto.ContentType);
                }
            }


            var resultado = await context.SaveChangesAsync();
            if (!(resultado > 0))
            {
                return false;
            }
            return true;
        }

        public async Task<ActorDTO?> ConsultarActorByIdAsync(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null) { return null; }

            var actorDTO = mapper.Map<ActorDTO>(actor);
            return actorDTO;

        }

        public async Task<List<ActorDTO>?> ConsultarActoresAsync()
        {
            var actores = await context.Actores.ToListAsync();
            if (actores is null)
            {
                return null;
            }

            var actoresDTO = mapper.Map<List<ActorDTO>>(actores);
            return actoresDTO;
        }

        public async Task<ActorDTO?> CrearActorAsync(ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actor.Foto = await almacenadorArchivos.GuardarArchivoAsync(contenido, extension, contenedor, actorCreacionDTO.Foto.ContentType);
                }
            }

            await context.Actores.AddAsync(actor);
            var resultado = await context.SaveChangesAsync();
            if (!(resultado > 0))
            {
                return null;
            }

            var _actorDTO = mapper.Map<ActorDTO>(actor);
            return _actorDTO;
        }

        public async Task<bool?> EliminarActorAsync(int id)
        {
            var existe = await context.Actores.AnyAsync(x => x.Id == id);
            if (!existe) { return false; }

            context.Actores.Remove(new Actor { Id = id });
            var resultado = await context.SaveChangesAsync();
            if (!(resultado > 0))
            {
                return false;
            }
            return true;
        }
    }
}
