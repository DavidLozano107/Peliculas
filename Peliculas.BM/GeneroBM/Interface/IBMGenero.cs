using Peliculas.DT.DTOs.GeneroDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.GeneroBM.Interface
{
    public interface IBMGenero
    {
        Task<List<GeneroDTO>?> ConsultarGenerosAsync();
        Task<GeneroDTO?> ConsultarGeneroByIdAsync(int id);
        Task<GeneroDTO?> CrearGeneroAsync(GeneroCreacionDTO generoCreacionDTO);
        Task<bool> ActualizacionGeneroAsync(int id, GeneroCreacionDTO generoCreacionDTO);
        Task<bool?> EliminarGeneroAsync(int id);
    }
}
