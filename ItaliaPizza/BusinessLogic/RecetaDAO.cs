using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class RecetaDAO : IReceta
    {
        public ResultadoOperacionEnum.ResultadoOperacion AddProductoVenta(Receta receta)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;


            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarReceta");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.Receta VALUES (@idReceta, @Nombre, @Procedimiento, @Rendimiento)";
                    command.Parameters.Add(new SqlParameter("@idReceta", receta.IdReceta));
                    command.Parameters.Add(new SqlParameter("@Nombre", receta.Nombre));
                    command.Parameters.Add(new SqlParameter("@Procedimiento", receta.Procedimiento));
                    command.Parameters.Add(new SqlParameter("@Rendimiento", receta.Rendimiento));

                    command.ExecuteNonQuery();

                    for (int posicion = 0; posicion < receta.Ingredientes.Count; posicion++)
                    {
                        command.CommandText =
                        "INSERT INTO dbo.RecetaIngrediente VALUES (@idReceta, @idProductoIngrediente, @cantidad, @PrecioUnitario)";
                        command.Parameters.Add(new SqlParameter("@idReceta", receta.IdReceta));
                        command.Parameters.Add(new SqlParameter("@idProductoIngrediente", receta.Ingredientes[posicion].Item1.Código));
                        command.Parameters.Add(new SqlParameter("@idCantidad", receta.Ingredientes[posicion].Item2));
                        command.Parameters.Add(new SqlParameter("@idPreciounitario", receta.Ingredientes[posicion].Item3));
                        command.ExecuteNonQuery();

                    }
                    transaction.Commit();
                    resultado = ResultadoOperacion.Exito;

                }
                catch (SqlException e)
                {
                    transaction.Rollback();

                    switch (e.Number)
                    {
                        case VALORES_DUPLICADOS:
                            resultado = ResultadoOperacion.ObjetoExistente;
                            break;
                        default:
                            resultado = ResultadoOperacion.FalloSQL;
                            break;
                    }
                }
            }
            return resultado;
        }

        public List<Receta> GetProductosIngrediente(int rango)
        {
            List<Receta> listaReceta = new List<Receta>();
            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    throw (ex);
                }
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Receta ORDER BY Nombre LIMIT 20 OFFSET @Rango", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Receta receta = new Receta();
                        receta.IdReceta = int.Parse(reader["Codigo"].ToString());
                        receta.Nombre = reader["Nombre"].ToString();

                        listaReceta.Add(receta);
                    }
                }
                connection.Close();
            }
            return listaReceta;
        }

        public Receta ObtenerProductoIngredientePorId(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
