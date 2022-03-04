using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Models
{
    public class EmpleadoINDto
    {
		
		[Required]
		[MaxLength(50)]
		public string Nombre { get; set; }
		
		[Required]
		[MaxLength(50)]
		public string ApellidoPaterno { get; set; }

		[Required]
		[MaxLength(50)]
		public string ApellidoMaterno { get; set; }
		
		[Required]
		public DateTime FechaNacimiento { get; set; }

		[Required]
		[MaxLength(10)]
		public string Genero { get; set; }
		[Required]
		[MaxLength(30)]
		public string EstadoCivil { get; set; }

		[Required]
		[MaxLength(13)]
		public string RFC { get; set; }

		[Required]
		[MaxLength(500)]
		public string Direccion { get; set; }

		[Required]
		[MaxLength(100)]
		public string Email { get; set; }

		[Required]
		[Range(1111111111, 9999999999)]
		public Int64 Telefono { get; set; }

		[Required]
		[MaxLength(100)]
		public string Puesto { get; set; }
		
	}
}
