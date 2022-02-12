using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DT.DTOs.PaginacionDTO
{
    public class PaginacionDTO
    {
        private int cantidadRegistrosPagina = 10;
        private readonly int cantidadRegistrosMaximoPorPagina = 50;

        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPagina
        {
            get => cantidadRegistrosPagina;
            set 
            {
                cantidadRegistrosPagina = (value > cantidadRegistrosMaximoPorPagina) ? cantidadRegistrosMaximoPorPagina : value;
            }
        }
    }
}
