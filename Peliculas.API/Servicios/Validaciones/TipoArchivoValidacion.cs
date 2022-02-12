using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DT.Validaciones
{
    public class TipoArchivoValidacion : ValidationAttribute
    {
        private readonly string[] tipoValidos;

        public TipoArchivoValidacion(string[] tipoValidos)
        {
            this.tipoValidos = tipoValidos;
        }

        public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                tipoValidos = new string[] {"imagen/jpeg", "imagen/jpg", "image/jpeg", "imagen/png", "imagen/gif","imagen/png", "image/png" };
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) { return ValidationResult.Success; }

            IFormFile? formFile = value as IFormFile;

            if (formFile is null) { return ValidationResult.Success; }
            
            if (!tipoValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: " +
                    $"{string.Join(", ", tipoValidos )}");
            }


            return ValidationResult.Success;
        }

    }
}
