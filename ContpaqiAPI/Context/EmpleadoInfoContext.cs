using ContpaqiAPI.DataAccess;
using ContpaqiAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Context
{
    public class EmpleadoInfoContext : IEmpleadoInfoContext
    {
        public List<Empleado> Lista()
        {
            List <Empleado>  ListaEmpleadoInfo = new List<Empleado>();
            DataReader dReader = SqlHelper.ExcecuteReader("prc_Empleado_sk", CommandType.StoredProcedure, "Contpaqi");

            //TODO: agregar validacion de error
            //if(dReader.ErrorId != 0)

            using (SqlDataReader reader = dReader.Reader)
            {
                while (reader.Read())
                {
                    ListaEmpleadoInfo.Add(Binder.BindData<Empleado>(reader));
                }
            }

            
            return ListaEmpleadoInfo;
        }

        public Empleado Agregar(Empleado empleado)
        {

            DataReader dReader = SqlHelper.ExcecuteReader("prc_Empleado_in", CommandType.StoredProcedure, empleado.SqlParameters, "Contpaqi");
            //TODO: agregar validacion de error

            using (SqlDataReader reader = dReader.Reader)
            {
                while (reader.Read())
                {
                    return (Binder.BindData<Empleado>(reader));
                }
            }


            return new Empleado();
        }


        public Empleado Editar(Empleado empleado)
        {

            DataReader dReader = SqlHelper.ExcecuteReader("prc_Empleado_up", CommandType.StoredProcedure, empleado.SqlParameters, "Contpaqi");
            //TODO: agregar validacion de error

            using (SqlDataReader reader = dReader.Reader)
            {
                while (reader.Read())
                {
                    return (Binder.BindData<Empleado>(reader));
                }
            }

            return new Empleado();
        }

        public Empleado Eliminar(int empleadoid)
        {

            SqlParameter sqlparam = new SqlParameter("@EmpleadoId", SqlDbType.Int);
            sqlparam.Value = empleadoid;
            List<SqlParameter> listparam = new List<SqlParameter>();
            listparam.Add(sqlparam);

            DataReader dReader = SqlHelper.ExcecuteReader("prc_Empleado_dl", CommandType.StoredProcedure, listparam, "Contpaqi");
            //TODO: agregar validacion de error

            using (SqlDataReader reader = dReader.Reader)
            {
                while (reader.Read())
                {
                    return (Binder.BindData<Empleado>(reader));
                }
            }

            return new Empleado();
        }

    }
}
