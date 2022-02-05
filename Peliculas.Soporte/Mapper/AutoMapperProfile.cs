using AutoMapper;
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
        }

    }
}
