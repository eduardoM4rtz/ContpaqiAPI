using ContpaqiAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Entities
{
    public class Empleado : ParametersBase
	{
		#region Declarations
		private int empleadoId;
		private string nombre;
		private string apellidoPaterno;
		private string apellidoMaterno;
		private int edad;
		private DateTime fechaNacimiento;
		private string genero;
		private string estadoCivil;
		private string rfc;
		private string direccion;
		private string email;
		private Int64 telefono;
		private string puesto;
		private DateTime? fechaAlta;
		private DateTime? fechaBaja;
		#endregion

		#region Constructors

		public Empleado()
            : base()
        { }

        #endregion

        #region Properties

		public int EmpleadoId
		{
			get { return empleadoId; }
			set
			{
				this.Parameters.AddPair("EmpleadoId", value);
				empleadoId = value;
			}
		}

	
		public string Nombre
		{
			get { return nombre; }
			set
			{
				this.Parameters.AddPair("Nombre", value);
				nombre = value;
			}
		}


		public string ApellidoPaterno
		{
			get { return apellidoPaterno; }
			set
			{
				this.Parameters.AddPair("ApellidoPaterno", value);
				apellidoPaterno = value;
			}
		}


		public string ApellidoMaterno
		{
			get { return apellidoMaterno; }
			set
			{
				this.Parameters.AddPair("ApellidoMaterno", value);
				apellidoMaterno = value;
			}
		}


		public int Edad
		{
			get { return edad; }
			set
			{
				this.Parameters.AddPair("Edad", value);
				edad = value;
			}
		}


		public DateTime FechaNacimiento
		{
			get { return fechaNacimiento; }
			set
			{
				this.Parameters.AddPair("FechaNacimiento", value);
				fechaNacimiento = value;
			}
		}


		public string Genero
		{
			get { return genero; }
			set
			{
				this.Parameters.AddPair("Genero", value);
				genero = value;
			}
		}

		public string EstadoCivil
		{
			get { return estadoCivil; }
			set
			{
				this.Parameters.AddPair("EstadoCivil", value);
				estadoCivil = value;
			}
		}

		public string RFC
		{
			get { return rfc; }
			set
			{
				this.Parameters.AddPair("RFC", value);
				rfc = value;
			}
		}

		public string Direccion
		{
			get { return direccion; }
			set
			{
				this.Parameters.AddPair("Direccion", value);
				direccion = value;
			}
		}

		public string Email
		{
			get { return email; }
			set
			{
				this.Parameters.AddPair("Email", value);
				email = value;
			}
		}


		public Int64 Telefono
		{
			get { return telefono; }
			set
			{
				this.Parameters.AddPair("Telefono", value);
				telefono = value;
			}
		}

		public string Puesto
		{
			get { return puesto; }
			set
			{
				this.Parameters.AddPair("Puesto", value);
				puesto = value;
			}
		}


		public DateTime? FechaAlta
		{
			get { return fechaAlta; }
			set
			{
				this.Parameters.AddPair("FechaAlta", value);
				fechaAlta = value;
			}
		}

		public DateTime? FechaBaja
		{
			get { return fechaBaja; }
			set
			{
				this.Parameters.AddPair("FechaBaja", value);
				fechaBaja = value;
			}
		}
		#endregion
	}
}
