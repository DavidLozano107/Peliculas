using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.BM.GeneroBM.Interface;
using Peliculas.DT.DTOs.GeneroDTOs;
using Peliculas.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.GeneroBM
{
    public class BMGenero : IBMGenero
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BMGenero(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> ActualizacionGeneroAsync(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;

            context.Entry(genero).State = EntityState.Modified;
            var resultado = await context.SaveChangesAsync();
            if (resultado > 0)
            {
                return true;
            }
            return false;

        }

        public async Task<GeneroDTO?> ConsultarGeneroByIdAsync(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if (genero is null) { return null; }

            var generoDTO = mapper.Map<GeneroDTO>(genero);
            return generoDTO;

        }

        public async Task<List<GeneroDTO>?> ConsultarGenerosAsync()
        {
            var listaGeneros = await context.Generos.ToListAsync();
            if (listaGeneros is null) { return null; }

            var listaGenerosDTO = mapper.Map<List<GeneroDTO>>(listaGeneros);
            return listaGenerosDTO;
        }

        public async Task<GeneroDTO?> CrearGeneroAsync(GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Generos.Add(genero);
            var resultado = await context.SaveChangesAsync();
            var generoDTO = mapper.Map<GeneroDTO>(genero);

            if (resultado > 0)
            {
                return generoDTO;
            }

            return null;
        }

        public async Task<bool?> EliminarGeneroAsync(int id)
        {
            var existe = await context.Generos.AnyAsync(x => x.Id == id);

            if (!existe) {return false;}

            context.Generos.Remove(new Genero { Id = id});
            var resultado = await context.SaveChangesAsync();
            
            if (!(resultado > 0))
            {
                return null;
            }
            return true;

        }
    }
}
