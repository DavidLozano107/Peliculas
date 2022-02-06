using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Soporte.Validaciones
{
    public class PesoArchivoValidacion: ValidationAttribute
    {
        private readonly int pesoMaximoEnMegaBytes;

        public PesoArchivoValidacion(int PesoMaximoEnMegaBytes)
        {
            pesoMaximoEnMegaBytes = PesoMaximoEnMegaBytes;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) { return ValidationResult.Success; }

            IFormFile formFile = value as IFormFile;
            if (formFile == null) { return ValidationResult.Success;  }


            int _pesoMaximoKiloBites = pesoMaximoEnMegaBytes * 1024 * 1024;

            if (formFile.Length > _pesoMaximoKiloBites)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {_pesoMaximoKiloBites}");
            }

            return ValidationResult.Success;


        }


    }
}
