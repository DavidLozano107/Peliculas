﻿using Microsoft.AspNetCore.Http;
using Peliculas.DT.Validaciones;
using Peliculas.Soporte.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DT.DTOs.ActorDTOs
{
    public class ActorCreacionDTO
    {
        [Required]
        [StringLength(140)]
        public string Nombre { get; set; } = String.Empty;
        public DateTime FechaNacimiento { get; set; }

        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
