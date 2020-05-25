using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class InventarioDAO : IInventario
    {
        public ResultadoOperacionEnum.ResultadoOperacion AddInventario(Inventario inventario)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();


            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("AgregarInventario");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.ProductoInventario VALUES (@Inventario, @Producto, @CantidadIngreso, @PrecioCompra, @FechaIngreso, @Caducidad)";
                    command.Parameters.Add(new SqlParameter("@Inventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@Producto", inventario.Producto.Código));
                    command.Parameters.Add(new SqlParameter("@CantidadIngreso", inventario.CantidadIngreso));
                    command.Parameters.Add(new SqlParameter("@PrecioCompra", inventario.PrecioCompra));
                    command.Parameters.Add(new SqlParameter("@FechaIngreso", inventario.FechaIngreso));
                    command.Parameters.Add(new SqlParameter("@Caducidad", inventario.Caducidad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.Inventario VALUES (@idInventario, @ExistenciaTotal, @UnidadMedida";
                    command.Parameters.Add(new SqlParameter("@idInventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@ExistenciaTotal", inventario.ExistenciaTotal));
                    command.Parameters.Add(new SqlParameter("@UnidadMedida", inventario.UnidadDeMedida));

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

        public ResultadoOperacion ModificarInventario(Inventario inventario)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();


            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("AgregarInventario");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "UPDATE dbo.ProductoInventario SET CantidadIngreso = @CantidadIngreso, PrecioCompra = @PrecioCompra," +
                         "FechaIngreso = @FechaIngreso, Caducidad = @Caducidad WHERE Producto = @Producto AND Inventario = @Inventario";
                    command.Parameters.Add(new SqlParameter("@Inventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@Producto", inventario.Producto.Código));
                    command.Parameters.Add(new SqlParameter("@CantidadIngreso", inventario.CantidadIngreso));
                    command.Parameters.Add(new SqlParameter("@PrecioCompra", inventario.PrecioCompra));
                    command.Parameters.Add(new SqlParameter("@FechaIngreso", inventario.FechaIngreso));
                    command.Parameters.Add(new SqlParameter("@Caducidad", inventario.Caducidad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "UPDATE dbo.Inventario SET ExistenciaTotal =  @ExistenciaTotal, UnidadMedida = Unidadmedida =  @UnidadMedida" +
                        "WHERE idInventario = @idInventario";
                    command.Parameters.Add(new SqlParameter("@idInventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@ExistenciaTotal", inventario.ExistenciaTotal));
                    command.Parameters.Add(new SqlParameter("@UnidadMedida", inventario.UnidadDeMedida));

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

        public List<Inventario> ObtenerInventarios(Producto producto)
        {
            List<Inventario> inventarios = new List<Inventario>();


            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
 
                    connection.Open();

                
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoInventario ORDER BY Nombre WHERE Producto = @Producto", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Producto", producto.Código));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inventario inventario = new Inventario();
                        inventario.CantidadIngreso = Convert.ToInt32( reader["CantidadIngreso"]);
                        inventario.PrecioCompra = float.Parse(reader["Precio"].ToString());
                        DateTime fechaIngreso = DateTime.Parse(reader["FechaIngreso"].ToString());
                        inventario.FechaIngreso = fechaIngreso;
                        DateTime caducidad = DateTime.Parse(reader["Caducidad"].ToString());
                        inventario.Caducidad = caducidad;
                        inventarios.Add(inventario);
                    }
                }
                connection.Close();
            }

            return inventarios;
        }

        public List<DataAccess.Inventario> ObtenerTodosLosInventarios()
        {
            List<DataAccess.Inventario> inventarios = new List<DataAccess.Inventario>();
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var inventario in context.Inventario)
                    {
                        inventario.Producto1 = inventario.Producto1;
                        inventarios.Add(inventario);
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los Inventarios");
                }
            }

            return inventarios;
        }
    }
}
