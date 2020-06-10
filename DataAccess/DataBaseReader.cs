using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public static class DataBaseReader
    {
        public static DataTable GetCrewData()
        {
            string query = "SELECT * FROM JS_BASE_INFO";
            using(OracleConnection connection = MyOracleConnetion.GetDBConnection())
            {
                OracleCommand command = new OracleCommand { CommandText = query, Connection = connection };
                OracleDataAdapter dataAdapter = new OracleDataAdapter { SelectCommand = command };
                DataTable dataTable = new DataTable();
                connection.Open();
                dataAdapter.Fill(dataTable);
                connection.Close();
                return dataTable;
            }
        }

        public static DataTable GetJsData()
        {
            string query = "SELECT * FROM JS_EXISTING_JS";
            using (OracleConnection connection = MyOracleConnetion.GetDBConnection())
            {
                OracleCommand command = new OracleCommand { CommandText = query, Connection = connection };
                OracleDataAdapter dataAdapter = new OracleDataAdapter { SelectCommand = command };
                DataTable dataTable = new DataTable();
                connection.Open();
                dataAdapter.Fill(dataTable);
                connection.Close();
                return dataTable;
            }
        }
    }
}
