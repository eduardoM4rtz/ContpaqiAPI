using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.DataAccess
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AttClass : Attribute
    {
        #region Declarations

        private bool reqVal;
        private string parameter;
        private object minVal;
        private object maxVal;
        private string regEx;

        #endregion

        #region Constructors

        public AttClass()
        {
            this.reqVal = false;
            this.parameter = String.Empty;
        }

        public AttClass(bool required)
        {
            this.reqVal = required;
            this.parameter = String.Empty;
        }

        public AttClass(string sqlParameter)
        {
            this.reqVal = false;
            this.parameter = sqlParameter;
        }

        public AttClass(bool required, string sqlParameter)
        {
            this.reqVal = required;
            this.parameter = sqlParameter;
        }

        #endregion

        #region Properties

        public bool ReqVal
        {
            get { return reqVal; }
            set { reqVal = value; }
        }

        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public object MinVal
        {
            get { return minVal; }
            set { minVal = value; }
        }

        public object MaxVal
        {
            get { return maxVal; }
            set { maxVal = value; }
        }

        public string RegEx
        {
            get { return regEx; }
            set { regEx = value; }
        }

        #endregion
    }
}
