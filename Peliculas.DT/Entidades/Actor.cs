using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DT.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = String.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string? Foto { get; set; }
    }
}
