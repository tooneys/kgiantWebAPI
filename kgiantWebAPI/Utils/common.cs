using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace kgiantWebAPI.Utils
{
    public class Common
    {
        public static String sqlDatoToJson(SqlDataReader dataReader)
        {
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
    }
}
