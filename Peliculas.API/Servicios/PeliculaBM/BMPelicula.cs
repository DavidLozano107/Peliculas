using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.API.Helper;
using Peliculas.BM.Helper;
using Peliculas.BM.Helper.AlmacenarArchivos.Interfaces;
using Peliculas.BM.PeliculaBM.Interface;
using Peliculas.DT.DTOs.PaginacionDTO;
using Peliculas.DT.DTOs.PeliculaDTOs;
using Peliculas.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.PeliculaBM
{
    public class BMPelicula : IBMPelicula
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly string contenedor = "peliculas";
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public BMPelicula(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        public async Task<bool?> ActualizacionPeliculaAsync(int id, PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDB = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (peliculaDB == null) { return null; }

            peliculaDB = mapper.Map(peliculaCreacionDTO, peliculaDB);


            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    peliculaDB.Poster = await almacenadorArchivos.EditarArchivoAsync(contenido,
                        extension,
                        contenedor,
                        peliculaDB.Poster,
                        peliculaCreacionDTO.Poster.ContentType);
                }
            }


            var resultado = await context.SaveChangesAsync();
            if (!(resultado > 0))
            {
                return false;
            }
            return true;
        }

        public async Task<PeliculaDTO?> ConsultarPeliculaByIdAsync(int id)
        {
            var peliculaDb = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (peliculaDb is null)
            {
                return null;
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(peliculaDb);
            return peliculaDTO;
        }

        public async Task<ResponsePaginador<PeliculaDTO, PeliculaEntidad>> ConsultarPeliculasAsync(PaginacionDTO paginacionDTO)
        {
            var querable = context.Peliculas.AsQueryable();

            var peliculas = await querable.Paginar(paginacionDTO).ToListAsync();

            if (peliculas is null)
            {
                return new ResponsePaginador<PeliculaDTO, PeliculaEntidad>()
                {
                    ListaResponse = new List<PeliculaDTO>(),
                    queryable = querable
                };
            }

            var peliculasDTO = mapper.Map<List<PeliculaDTO>>(peliculas);
            return new ResponsePaginador<PeliculaDTO, PeliculaEntidad>() { ListaResponse = peliculasDTO, queryable = querable };
        }

        public async Task<PeliculaDTO?> CrearPeliculaAsync(PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<PeliculaEntidad>(peliculaCreacionDTO);

            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    pelicula.Poster = await almacenadorArchivos.GuardarArchivoAsync(contenido, extension, contenedor, peliculaCreacionDTO.Poster.ContentType);
                }
            }

            await context.Peliculas.AddAsync(pelicula);
            var resultado = await context.SaveChangesAsync();


            if (!(resultado > 0))
            {
                return null;
            }

            var _peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            return _peliculaDTO;

        }

        public async Task<bool?> EliminarPeliculaAsync(int id)
        {
            var peliculaDb = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (peliculaDb == null) { return null; }

            context.Peliculas.Remove(peliculaDb);

            var resultado = await context.SaveChangesAsync();
            if (resultado > 0)
            {
                return true;
            }
            return false;
        }
    }
}
