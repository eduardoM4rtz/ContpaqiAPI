using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContpaqiAPI.DataAccess
{
    public abstract class SqlHelper
    {
       
        private static string GetConnectionString(string name)
        {
            string connectionstring = Startup._iConfiguration["DBConnections:SqliteConnectionString"];
           
            return connectionstring;
        }

        public static DataReader ExcecuteReader(string commandText, CommandType commandType, string connStrName)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetConnectionString(connStrName));

            try
            {
                ConfigCommand(conn, cmd, commandType, commandText, null, new List<SqlParameter>());
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataReader dReader = new DataReader();
                dReader.Reader = reader;
                dReader.ErrorId = Convert.ToInt32(cmd.Parameters["@ErrorId"].Value);
                dReader.Error = Convert.ToString(cmd.Parameters["@Error"].Value);

                return dReader;
            }

            catch(Exception ex)
            {
                conn.Close();
                throw;
            }

            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public static DataReader ExcecuteReader(string commandText, CommandType commandType, List<SqlParameter> parameters, string connStrName)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetConnectionString(connStrName));

            try
            {
                ConfigCommand(conn, cmd, commandType, commandText, null, parameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataReader dReader = new DataReader();
                dReader.Reader = reader;
                dReader.ErrorId = Convert.ToInt32(cmd.Parameters["@ErrorId"].Value);
                dReader.Error = Convert.ToString(cmd.Parameters["@Error"].Value);

                return dReader;
            }

            catch
            {
                conn.Close();
                throw;
            }

            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public static int ExcecuteNonQuery(string commandText, CommandType commandType, SqlParameter parameter, string connStrName)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetConnectionString(connStrName));
            List<SqlParameter> parametersList = new List<SqlParameter>();

            try
            {
                parametersList.Add(parameter);
                ConfigCommand(conn, cmd, commandType, commandText, null, parametersList);
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }

            catch
            {
                conn.Close();
                throw;
            }

            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
            }
        }

        public static int ExcecuteNonQuery(string commandText, CommandType commandType, List<SqlParameter> parameters, string connStrName)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetConnectionString(connStrName));

            try
            {
                ConfigCommand(conn, cmd, commandType, commandText, null, parameters);
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }

            catch
            {
                conn.Close();
                throw;
            }

            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
            }
        }

        private static void ConfigCommand(SqlConnection conn, SqlCommand cmd, CommandType commandType, string commandText, SqlTransaction transaction, List<SqlParameter> parameters)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = commandText;

            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }

            cmd.CommandType = commandType;
            cmd.AddSqlParameters(parameters);
        }


    }

    public static class SqlCommandExt
    {
        /// <summary>
        /// Agrega los SqlParameter de la lista al SqlCommand.
        /// </summary>
        /// <param name="cmd">SqlCommand al que se le agregarán los SqlParameter.</param>
        /// <param name="sqlParameters">Lista de SqlParameter que serán agregados al SqlCommand.</param>
        public static void AddSqlParameters(this SqlCommand cmd, List<SqlParameter> sqlParameters)
        {
            foreach (SqlParameter parameter in sqlParameters)
            {
                cmd.Parameters.Add(parameter);
            }

            //EML se agregan parametros de salida
            SqlParameter ErrorIdParameter = new SqlParameter();
            ErrorIdParameter.ParameterName = "@ErrorId";
            ErrorIdParameter.SqlDbType = System.Data.SqlDbType.Int;
            ErrorIdParameter.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(ErrorIdParameter);

            SqlParameter ErrorParameter = new SqlParameter();
            ErrorParameter.ParameterName = "@Error";
            ErrorParameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            ErrorParameter.Size = 4000;
            ErrorParameter.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(ErrorParameter);
        }
    }

    public abstract class Binder
    {
        /// <summary>
        /// Crea una instancia de un objeto del tipo T y le asigna los valores del SqlDataReader.
        /// </summary>
        /// <typeparam name="T">El tipo del objeto a instanciar.</typeparam>
        /// <param name="reader">SqlDataReader con la información para el objeto.</param>
        /// <returns>Un objeto del tipo T con la información del reader.</returns>
        public static T BindData<T>(SqlDataReader reader)
        {
            object obj = Activator.CreateInstance<T>();
            reader.ObjectDataBind(obj);
            return (T)obj;
        }
    }


    public static class SqlDataReaderExt
    {
        /// <summary>
        /// Asigna los valores del SqlDataReader a las propiedades correspondientes del objeto indicado.
        /// </summary>
        /// <param name="reader">Éste SqlDataReader.</param>
        /// <param name="x">Objeto al cual se le asignarán los valores del SqlDataReader a las propiedades correspondientes.</param>
        public static void ObjectDataBind(this SqlDataReader reader, object x)
        {
            string propertyName;
            object propertyValue;
            PropertyInfo propertyInfo;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                propertyName = reader.GetName(i);
                propertyValue = reader[i];
                propertyInfo = x.GetType().GetProperty(propertyName);

                if (x.GetType().GetProperty(propertyName) == null)
                {
                    throw new NullReferenceException("El objeto no tiene una propiedad " + '"' + propertyName + '"');
                }

                if (!string.IsNullOrEmpty(reader[i].ToString()))
                {
                    try
                    {
                        string propertyType = propertyInfo.PropertyType.Name.ToLower();
                        if (propertyType.Equals("int64"))
                        {
                            bool success = Int64.TryParse(propertyValue.ToString(), out long number);
                            if (success)
                            {
                                propertyInfo.SetValue(x, number, null);
                            }
                            else
                                throw new ArgumentException("Error al asignar el valor de la propiedad '" + propertyName + "'. ");
                        }
                        else
                            propertyInfo.SetValue(x, propertyValue, null);
                    }

                    catch (Exception ex)
                    {
                        throw new ArgumentException("Error al asignar el valor de la propiedad '" + propertyName + "'. " + ex.Message);
                    }
                }
                
                else
                {
                    string propertyType = propertyInfo.PropertyType.Name.ToLower();
                    if (propertyType.Equals("int32") || propertyType.Equals("int64") || propertyType.Equals("int16"))
                    {
                        //propertyInfo.SetValue(x, 0, null);
                        propertyInfo.SetValue(x, null, null);
                    }
                    else if (propertyType.Equals("string"))
                    {
                        propertyInfo.SetValue(x, string.Empty, null);
                    }
                    else if (propertyType.Equals("double"))
                    {
                        //propertyInfo.SetValue(x, 0.0, null);
                        propertyInfo.SetValue(x, null, null);
                    }
                    else if (propertyType.Equals("boolean"))
                    {
                        propertyInfo.SetValue(x, false, null);
                    }
                    else if (propertyType.Equals("datetime"))
                    {
                        //propertyInfo.SetValue(x, DateTime.MinValue, null);
                        propertyInfo.SetValue(x, null, null);
                    }
                    else if (propertyType.Equals("single"))
                    {
                        propertyInfo.SetValue(x, 0.0f, null);
                    }
                    else if (propertyType.Equals("char"))
                    {
                        propertyInfo.SetValue(x, ' ', null);
                    }
                    else if (propertyType.Equals("decimal"))
                    {
                        propertyInfo.SetValue(x, 0.0m, null);
                    }
                }
            }
        }
    }
}


