using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContpaqiAPI.DataAccess
{
    [Serializable]
    public abstract class ParametersBase : ModelBase
    {
        #region Declarations        
        Dictionary<string, object> parameters;

        #endregion

        #region Constructors

        public ParametersBase()
        {
            parameters = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Un diccionario de string, object con los parametros para la base de datos.
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Una lista de SqlParameter con los parametros para la base de datos.
        /// </summary>
        public List<SqlParameter> SqlParameters
        {
            get
            {
                return GetParameters();
            }
        }

        /// <summary>
        /// Un SqlParameter "@XmlString" con una cadena en formato XML de los parametros para la base de datos como valor.
        /// </summary>
        public SqlParameter SqlParametersXml
        {
            get
            {
                return new SqlParameter("@XmlString", ParametersXml);
            }
        }

        /// <summary>
        /// Una cadena en formato XML de los parametros para la base de datos.
        /// </summary>
        public string ParametersXml
        {
            get
            {
                return GetParametersXml();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Regresa una lista con los parametros de Sql.
        /// </summary>
        /// <returns>Una lista de SqlParameters.</returns>
        private List<SqlParameter> GetParameters()
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (parameter.Value != null)
                {
                    if (!parameter.Value.GetType().FullName.Contains(".Tools.Collections.HelpList"))
                    {
                        sqlParameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                    }

                    else
                    {
                        sqlParameters.Add(new SqlParameter("@" + parameter.Key, GetHelpListXml(parameter)));
                    }
                }
            }

            return sqlParameters;
        }

        /// <summary>
        /// Regresa un string en formato xml con los parametros de la lista.
        /// </summary>
        /// <param name="parameter">Un KeyValuePair de string, object que sea del tipo DataHelper.HelpList.</param>
        /// <returns>Un string en formato xml.</returns>
        private string GetHelpListXml(KeyValuePair<string, object> parameter)
        {
            XElement element = new XElement(GetType().Name);

            foreach (XElement e in (List<XElement>)parameter.Value.GetType().GetProperty("XmlParametersList").GetValue(parameter.Value, null))
            {
                element.Add(e);
            }

            return element.ToString();
        }

        /// <summary>
        /// Regresa un string en formato xml con los parametros de la clase.
        /// </summary>
        /// <returns>Un string en formato xml.</returns>
        private string GetParametersXml()
        {
            XElement element = new XElement(GetType().Name);

            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (parameter.Value != null)
                {
                    if (parameter.Value.GetType().FullName.Contains(".Tools.Collections.HelpList"))
                    {
                        List<XElement> listElements = (List<XElement>)parameter.Value.GetType().GetProperty("XmlParametersList").GetValue(parameter.Value, null);

                        foreach (XElement e in listElements)
                        {
                            element.Add(e);
                        }
                    }

                    else
                    {
                        element.SetAttributeValue(parameter.Key, parameter.Value);
                    }
                }
            }

            return element.ToString();
        }

        #endregion
    }



    public static class DictionaryExt
    {
        /// <summary>
        /// Agrega el par de string, object al Dictionary. Si ya existe otro par con el mismo "key" simplemente reemplaza el valor.
        /// </summary>
        /// <param name="dic">Éste Dictonary.</param>
        /// <param name="key">String con el "key" del par.</param>
        /// <param name="value">Object con el "value" del par.</param>
        public static void AddPair(this Dictionary<string, object> dic, string key, object value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }

            else
            {
                dic.Add(key, value);
            }
        }
    }
}
