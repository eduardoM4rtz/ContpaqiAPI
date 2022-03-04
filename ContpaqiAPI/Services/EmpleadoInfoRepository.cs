using ContpaqiAPI.Context;
using ContpaqiAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Services
{
    public class EmpleadoInfoRepository : IEmpleadoInfoRepository
    {
        private IEmpleadoInfoContext _context;

        public EmpleadoInfoRepository(IEmpleadoInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Empleado> GetEmpleados()
        {
            return _context.Lista().OrderBy(m => m.Nombre).ToList();
        }

        public Empleado GetEmpleado(int empleadoId)
        {
            return _context.Lista().Where(x => x.EmpleadoId == empleadoId).FirstOrDefault();
        }

        public Empleado AddEmpleado(Empleado empleado)
        {
            return _context.Agregar(empleado);
        }

        public Empleado UpdateEmpleado(Empleado empleado)
        {
            return _context.Editar(empleado);
        }

        public Empleado DeleteEmpleado(int empleadoId)
        {
            return _context.Eliminar(empleadoId);
        }
    }
}
