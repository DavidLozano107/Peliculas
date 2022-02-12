using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DT.Entidades
{
    public class PeliculasGeneros
    {
        public int PeliculaId { get; set; }
        public int GeneroId { get; set; }

        public PeliculaEntidad Pelicula { get; set; }
        public Genero Genero { get; set; }
    }
}
