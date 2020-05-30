using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnection;

namespace BusinessLogic
{
    public class DatabaseDAO
    {
        public ResultadoOperacionEnum.ResultadoOperacion ObtenerRespaldo(String ruta)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();
            string comando = "BACKUP DATABASE Pizza TO DISK = '" + ruta + "Pizza-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

            using (SqlConnection sqlConnection = dbConnection.GetConnection())
            {
                sqlConnection.Open();

                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    resultado = ResultadoOperacionEnum.ResultadoOperacion.Exito;
                }
                catch (SqlException)
                {
                    resultado = ResultadoOperacionEnum.ResultadoOperacion.FalloSQL;
                }
            }
            return resultado;
        }
    }
}
