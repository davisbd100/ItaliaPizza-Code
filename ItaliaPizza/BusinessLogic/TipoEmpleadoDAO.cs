using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseConnection;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class TipoEmpleadoDAO : ITipoEmpleado
    {
        public ResultadoOperacionEnum.ResultadoOperacion AgregarTipoEmpleado(TipoEmpleado tipoEmpleado)
        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.TipoEmpleado VALUES (@idEmpleado, @TipoEmpleado)", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idTipoEmpleado", tipoEmpleado.idTipoEmpleado));
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", tipoEmpleado.TipoEmp));

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
