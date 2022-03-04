using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Models
{
    public class EmpleadoUPDto
    {
		[Required]
		public int? EmpleadoId { get; set; }

		[MaxLength(100)]
		public string Email { get; set; }

		[Range(1111111111, 9999999999)]
		public Int64 Telefono { get; set; }

		[MaxLength(100)]
		public string Puesto { get; set; }

		public DateTime? FechaBaja { get; set; }

		[MaxLength(30)]
		public string EstadoCivil { get; set; }

		[MaxLength(500)]
		public string Direccion { get; set; }

	}
}
