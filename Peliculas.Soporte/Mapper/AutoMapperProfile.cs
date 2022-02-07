using AutoMapper;
using Peliculas.DT.DTOs.ActorDTOs;
using Peliculas.DT.DTOs.GeneroDTOs;
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
            CreateMap<Genero,GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO,Genero>();

            CreateMap<Actor,ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, op => op.Ignore());

            CreateMap<ActorPathcDTO, Actor>().ReverseMap();
        }

    }
}
