using Microsoft.AspNetCore.JsonPatch;
using Peliculas.DT.DTOs.ActorDTOs;
using Peliculas.DT.DTOs.PaginacionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.ActorBM.Interface
{
    public interface IBMActor
    {
        Task<List<ActorDTO>?> ConsultarActoresAsync(PaginacionDTO paginacionDTO);
        Task<ActorDTO?> ConsultarActorByIdAsync(int id);
        Task<ActorDTO?> CrearActorAsync(ActorCreacionDTO actorCreacionDTO);
        Task<bool?> ActualizacionActorAsync(int id, ActorCreacionDTO actorCreacionDTO);
        Task<bool?> EliminarActorAsync(int id);
       
    }
}
