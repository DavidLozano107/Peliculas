using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.BM.GeneroBM.Interface;
using Peliculas.DT.DTOs.GeneroDTOs;
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

        public BMGenero(ApplicationDbContext context )
        {
            this.context = context;
        }
        public async Task<GeneroDTO> ConsultarGeneroByIdAsync(int id)
        {
            //var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);  
            //return genero;
            return null;

        }

        public async Task<List<GeneroDTO>> ConsultarGenerosAsync()
        {
            return null;
        }

        public async Task<bool> CrearGeneroAsync(GeneroCreacionDTO generoCreacionDTO)
        {
            return false;
        }
    }
}
