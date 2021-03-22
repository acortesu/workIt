using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Dao
{
    public class SqlDao
    {
        private string CONNECTION_STRING = "";

        private static SqlDao instance;

        private SqlDao()
        {
            CONNECTION_STRING = ConfigurationManager.ConnectionStrings["WORK_IT_CONN_STRING"].ConnectionString;
        }

        /// <summary>
        /// Retorna la unica que instancia de intance y si no se a creado
        /// la crea
        /// </summary>
        /// <returns> instacia unica de SqlDao</returns>
        public static SqlDao GetInstance()
        {
            return instance ?? (instance = new SqlDao());
        }


        public void ExecuteProcedure(SqlOperation sqlOperation)
        {
            using (var conn = new SqlConnection(CONNECTION_STRING))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var lstResult = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(CONNECTION_STRING))

            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        for (var colIndex = 0; colIndex < reader.FieldCount; colIndex++)
                        {
                            dict.Add(reader.GetName(colIndex), reader.GetValue(colIndex));
                        }

                        lstResult.Add(dict);
                    }
                }
            }

            return lstResult;
        }
    }
}