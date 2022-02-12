using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Peliculas.BM.Helper.AlmacenarArchivos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.Helper.GuardarArchivos
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {

        private readonly string ConnectionString;

        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("AzureStorange");
        }

        public async Task BorrarArchivoAsync(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            var cliente = new BlobContainerClient(ConnectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();

            var archivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(archivo);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditarArchivoAsync(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {

            await BorrarArchivoAsync(ruta, contenedor);
            return await GuardarArchivoAsync(contenido,extension,contenedor,contentType);
        }

        public async Task<string> GuardarArchivoAsync(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var cliente = new BlobContainerClient(ConnectionString, contenedor);

            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var archivoNombre = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(archivoNombre);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(contenido), blobUploadOptions);
            return blob.Uri.ToString(); 
        }
    }
}
