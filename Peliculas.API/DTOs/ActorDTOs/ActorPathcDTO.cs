using Peliculas.Soporte.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DT.DTOs.ActorDTOs
{
    public class ActorPathcDTO
    {
        [Required]
        [StringLength(140)]
        public string Nombre { get; set; } = String.Empty;
        public DateTime FechaNacimiento { get; set; }
    }
}
