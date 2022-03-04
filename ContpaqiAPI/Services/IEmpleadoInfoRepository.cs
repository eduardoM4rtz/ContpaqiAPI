using ContpaqiAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Services
{
    public interface IEmpleadoInfoRepository
    {
        IEnumerable<Empleado> GetEmpleados();
        Empleado GetEmpleado(int empleadoId);
        Empleado AddEmpleado(Empleado empleado);
        Empleado UpdateEmpleado(Empleado empleado);

        Empleado DeleteEmpleado(int empleadoId);
    }
}
