using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class TipoProductoDAO : ITipoProducto
    {
        public ResultadoOperacionEnum.ResultadoOperacion addTipoProducto(TipoProducto tipoProducto)
        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.TipoProducto VALUES (@idProducto, @NombreProducto)", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idTipoProducto", tipoProducto.IdTipoProducto));
                    command.Parameters.Add(new SqlParameter("@NombreTipoProducto", tipoProducto.NombreTipoProducto));

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                    }
                    catch (SqlException)
                    {
                        resultado = ResultadoOperacion.FalloSQL;
                        return resultado;
                    }
                    resultado = ResultadoOperacion.Exito;
                }
            }
            return resultado;
        }
    }
}
