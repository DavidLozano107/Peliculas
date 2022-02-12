using Peliculas.BM.Helper;
using Peliculas.DT.DTOs.PaginacionDTO;
using Peliculas.DT.DTOs.PeliculaDTOs;
using Peliculas.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.PeliculaBM.Interface
{
    public interface IBMPelicula
    {
        Task<ResponsePaginador<PeliculaDTO,PeliculaEntidad>> ConsultarPeliculasAsync(PaginacionDTO paginacionDTO);
        Task<PeliculaDTO?> ConsultarPeliculaByIdAsync(int id);
        Task<PeliculaDTO?> CrearPeliculaAsync(PeliculaCreacionDTO peliculaCreacionDTO);
        Task<bool?> ActualizacionPeliculaAsync(int id, PeliculaCreacionDTO peliculaCreacionDTO);
        Task<bool?> EliminarPeliculaAsync(int id);
    }
}
