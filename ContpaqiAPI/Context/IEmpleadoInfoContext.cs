using ContpaqiAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Context
{
    public interface IEmpleadoInfoContext
    {
        List<Empleado> Lista();

        Empleado Agregar(Empleado empleado);

        Empleado Editar(Empleado empleado);

        Empleado Eliminar(int empleadoid);
    }
}
