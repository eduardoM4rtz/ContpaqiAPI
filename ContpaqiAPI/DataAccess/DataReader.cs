using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.DataAccess
{
    public class DataReader 
    {
        public SqlDataReader Reader { get; set; }
        public int ErrorId { get; set; }
        public string Error { get; set; }

    }
}
