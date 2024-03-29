﻿using Peliculas.DT.DTOs.PaginacionDTO;

namespace Peliculas.API.Helper
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            return queryable
               .Skip((paginacionDTO.Pagina - 1) * paginacionDTO.CantidadRegistrosPagina )
               .Take(paginacionDTO.CantidadRegistrosPagina);
        }
    }
}
