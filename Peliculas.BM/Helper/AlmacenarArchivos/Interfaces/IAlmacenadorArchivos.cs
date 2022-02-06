using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.Helper.AlmacenarArchivos.Interfaces
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivoAsync(byte[] contenido, string extension, string contenedor, string contentType);

        Task<string> EditarArchivoAsync(byte[] contenido, string extension, string contenedor, string ruta, string contentType);

        Task BorrarArchivoAsync(string ruta, string contenedor);
    }
}
