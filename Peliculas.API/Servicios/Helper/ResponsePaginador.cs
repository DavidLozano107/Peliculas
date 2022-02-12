using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.Helper
{
    public class ResponsePaginador<T,Y>
    {
        public List<T>? ListaResponse { get; set; }
        public IQueryable<Y> queryable { get; set; }
    }
}
