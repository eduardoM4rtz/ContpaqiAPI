using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContpaqiAPI.DataAccess
{

    public class ModelBase
    {
        private PropertyInfo[] objProperties
        {
            get
            {
                return this.GetType().GetProperties();
            }
        }

        public bool ValidateProperties()
        {
            foreach (PropertyInfo property in objProperties)
            {
                ValidateInnerProperties(this, property);
            }
            return true;
        }

        private void ValidateInnerProperties(object instance, PropertyInfo property)
        {
            AttClass attributes;
            Type myType = property.PropertyType;
            PropertyInfo[] arr3 = myType.GetProperties();
            if (arr3.Length > 0)
            {
                foreach (PropertyInfo prop in arr3)
                {
                    ParameterInfo[] par = property.GetIndexParameters();
                    //ValidateInnerProperties(property.GetValue(instance, par.Length>0? new Object[]{1}:null), prop);
                    //Cambio de index para colecciones con un elemento 
                    try//Validar caso de objetos nulos
                    {

                        if (instance.GetType().IsGenericType && instance is IEnumerable)
                        {
                            int count = 0;
                            foreach (object ob in ((IEnumerable)instance))
                                count++;
                            if (count > 0)
                                ValidateInnerProperties(property.GetValue(instance, par.Length > 0 ? new Object[] { 0 } : null), prop);
                        }
                        else
                            ValidateInnerProperties(property.GetValue(instance, par.Length > 0 ? new Object[] { 0 } : null), prop);
                    }
                    catch (NullReferenceException ex)
                    {
                    }
                }
            }
            attributes = (AttClass)Attribute.GetCustomAttribute(property, typeof(AttClass));
            if (attributes != null)
            {
                object value = property.GetValue(instance, null);
                if (attributes.ReqVal)
                {
                    //ValidateRequiredValue(property.GetValue(instance, null), property.Name);
                    ValidateRequiredValue(value, property.Name);
                }
                if (attributes.MinVal != null | attributes.MaxVal != null)
                {
                    //ValidateRangeValue(property.GetValue(instance, null), property.Name, attributes.MinVal, attributes.MaxVal);
                    ValidateRangeValue(value, property.Name, attributes.MinVal, attributes.MaxVal);
                }
                if (!String.IsNullOrEmpty(attributes.RegEx))
                {
                    //ValidateRangeValue(property.GetValue(instance, null), property.Name, attributes.MinVal, attributes.MaxVal);
                    ValidateRegExValue(value, property.Name, attributes.RegEx);
                }
            }
        }

        private void ValidateRequiredValue(object value, string propName)
        {

            if (value == null)
            {
                throw new ArgumentNullException(propName);
            }
            else
            {
                if (value.GetType().IsGenericType && value is IEnumerable)
                {
                    int count = 0;
                    foreach (object ob in ((IEnumerable)value))
                        count++;
                    if (count == 0)
                        throw new ArgumentNullException(propName);
                }
                if (value.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(value)) || Convert.ToString(value).Equals(""))
                    {
                        throw new ArgumentNullException(propName);
                    }
                }
                if (value.GetType() == typeof(int))
                {
                    if (Convert.ToInt32(value).Equals(0))
                    {
                        throw new ArgumentNullException(propName);
                    }
                }
                if (value.GetType() == typeof(decimal))
                {
                    if (Convert.ToDecimal(value).Equals(0.0))
                    {
                        throw new ArgumentNullException(propName);
                    }
                }
            }

        }

        private void ValidateRangeValue(object value, string propName, object minVal, object maxVal)
        {
            if (value != null)
            {
                bool error = false;
                if (value.GetType() == typeof(DateTime))
                {
                    if (minVal == null && maxVal != null)
                    {
                        error = Convert.ToDateTime(value) <= Convert.ToDateTime(maxVal) ? false : true;
                    }
                    else if (minVal != null && maxVal == null)
                    {
                        error = Convert.ToDateTime(value) >= Convert.ToDateTime(minVal) ? false : true;
                    }
                    else
                    {
                        error = Convert.ToDateTime(value) >= Convert.ToDateTime(minVal) && Convert.ToDateTime(value) <= Convert.ToDateTime(maxVal) ? false : true;
                    }
                }
                if (value.GetType() == typeof(int))
                {
                    if (minVal == null && maxVal != null)
                    {
                        error = Convert.ToInt32(value) <= Convert.ToInt32(maxVal) ? false : true;
                    }
                    else if (minVal != null && maxVal == null)
                    {
                        error = Convert.ToInt32(value) >= Convert.ToInt32(minVal) ? false : true;
                    }
                    else
                    {
                        error = Convert.ToInt32(value) >= Convert.ToInt32(minVal) && Convert.ToInt32(value) <= Convert.ToInt32(maxVal) ? false : true;
                    }
                }
                if (value.GetType() == typeof(decimal))
                {
                    if (minVal == null && maxVal != null)
                    {
                        error = Convert.ToDecimal(value) <= Convert.ToDecimal(maxVal) ? false : true;
                    }
                    else if (minVal != null && maxVal == null)
                    {
                        error = Convert.ToDecimal(value) >= Convert.ToDecimal(minVal) ? false : true;
                    }
                    else
                    {
                        error = Convert.ToDecimal(value) >= Convert.ToDecimal(minVal) && Convert.ToDecimal(value) <= Convert.ToDecimal(maxVal) ? false : true;
                    }
                }
                if (value.GetType() == typeof(double))
                {
                    if (minVal == null && maxVal != null)
                    {
                        error = Convert.ToDouble(value) <= Convert.ToDouble(maxVal) ? false : true;
                    }
                    else if (minVal != null && maxVal == null)
                    {
                        error = Convert.ToDouble(value) >= Convert.ToDouble(minVal) ? false : true;
                    }
                    else
                    {
                        error = Convert.ToDouble(value) >= Convert.ToDouble(minVal) && Convert.ToDouble(value) <= Convert.ToDouble(maxVal) ? false : true;
                    }
                }
                if (value.GetType() == typeof(string) && !string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    if (minVal == null && maxVal != null)
                    {
                        error = Convert.ToString(value).Length <= Convert.ToInt32(maxVal) ? false : true;
                    }
                    else if (minVal != null && maxVal == null)
                    {
                        error = Convert.ToString(value).Length >= Convert.ToInt32(minVal) ? false : true;
                    }
                    else
                    {
                        error = Convert.ToString(value).Length >= Convert.ToInt32(minVal) && Convert.ToString(value).Length <= Convert.ToInt32(maxVal) ? false : true;
                    }
                }
                if (error)
                {
                    throw new ArgumentOutOfRangeException(propName);
                }
            }
        }

        private void ValidateRegExValue(object value, string propName, string regEx)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                string exp = value.ToString();

                if (!Regex.IsMatch(exp, regEx))
                {
                    throw new FormatException(propName);
                }
            }
        }
    }
}

