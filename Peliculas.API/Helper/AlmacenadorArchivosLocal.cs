using Microsoft.AspNetCore.Hosting;
using Peliculas.BM.Helper.AlmacenarArchivos.Interfaces;

namespace Peliculas.API.Helper
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task BorrarArchivoAsync(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);
                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivoAsync(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivoAsync(ruta, contenedor);
            return await GuardarArchivoAsync(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivoAsync(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);


            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlDb = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");
            return urlDb;
        }
    }
}
