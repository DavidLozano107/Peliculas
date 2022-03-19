using AutoMapper;
using Peliculas.DT.DTOs.ActorDTOs;
using Peliculas.DT.DTOs.GeneroDTOs;
using Peliculas.DT.DTOs.PeliculaDTOs;
using Peliculas.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Soporte.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, op => op.Ignore());

            CreateMap<ActorPathcDTO, Actor>().ReverseMap();

            CreateMap<PeliculaEntidad, PeliculaDTO>().ReverseMap();

            CreateMap<PeliculaCreacionDTO, PeliculaEntidad>()
                .ForMember(x => x.Poster, opciones => opciones.Ignore())
                .ForMember(x => x.PeliculasGeneros, op => op.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculasActores, op => op.MapFrom(MapPeliculasGeneros));


            CreateMap<PeliculaPatchDTO, PeliculaEntidad>().ReverseMap();




        }


        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, PeliculaEntidad pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if (peliculaCreacionDTO.GeneroIDs == null)
            {
                return resultado;
            }


            foreach (var id in peliculaCreacionDTO.GeneroIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }
            return resultado;
        }

        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, PeliculaEntidad pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDTO.GeneroIDs == null)
            {
                return resultado;
            }


            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId, Personaje = actor.Personaje, });
            }
            return resultado;
        }

    }
}
