using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class RecetaDAO : IReceta
    {
        public ResultadoOperacionEnum.ResultadoOperacion AddReceta(Receta receta)
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
                        "INSERT INTO dbo.RecetaIngrediente VALUES (@idReceta, @idProductoIngrediente, @Cantidad, @PrecioUnitario)";
                        command.Parameters.Add(new SqlParameter("@idReceta", receta.IdReceta));
                        command.Parameters.Add(new SqlParameter("@idProductoIngrediente", receta.Ingredientes[posicion].IdIngrediente));
                        command.Parameters.Add(new SqlParameter("@Cantidad", receta.Ingredientes[posicion].Cantidad));
                        command.Parameters.Add(new SqlParameter("@Preciounitario", receta.Ingredientes[posicion].PrecioUnitario));
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

        public List<Receta> GetRecetas(int rango)
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

        public Receta ObtenerRecetaPorId(int idReceta)
        {
            Receta receta = new Receta();

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
                         "SELECT * From dbo.Receta WHERE idReceta = @idReceta";
                    command.Parameters.Add(new SqlParameter("@idReceta", idReceta));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        receta.IdReceta = int.Parse(reader["Codigo"].ToString());
                        receta.Nombre = reader["Nombre"].ToString();
                        receta.Procedimiento = reader["Procedimienti"].ToString();
                        receta.Rendimiento = float.Parse(reader["Rendimiento"].ToString());

                    }

                    command.CommandText =
                        "SELECT * FROM dbo.RecetaIngrediente WHERE idReceta = @idReceta";
                    command.Parameters.Add(new SqlParameter("@idReceta", receta.IdReceta));
                    SqlDataReader reader2 = command.ExecuteReader();

                    while (reader2.Read())
                    {
                        ListaIngredientesReceta listaIngredientesReceta = new ListaIngredientesReceta();
                        listaIngredientesReceta.IdIngrediente = int.Parse(reader["idProductoIngrediente"].ToString());
                        listaIngredientesReceta.Cantidad = int.Parse(reader["Cantidad"].ToString());
                        listaIngredientesReceta.PrecioUnitario = float.Parse(reader["PrecioUnitario"].ToString());

                        receta.Ingredientes.Add(listaIngredientesReceta);

                    }
                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }
            return receta;

        }
    }
}
