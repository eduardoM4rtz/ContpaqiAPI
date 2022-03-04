using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Models
{
	public class EmpleadoSKDto
	{
		public int EmpleadoId { get; set; }

		public string Nombre { get; set; }

		public string ApellidoPaterno { get; set; }

		public string ApellidoMaterno { get; set; }

		public int Edad { get; set; }

		public DateTime FechaNacimiento { get; set; }

		public string Genero { get; set; }

		public string EstadoCivil { get; set; }

		public string RFC { get; set; }

		public string Direccion { get; set; }

		public string Email { get; set; }

		public Int64 Telefono { get; set; }

		public string Puesto { get; set; }

		public DateTime FechaAlta { get; set; }

		public DateTime? FechaBaja { get; set; }

	}
}
