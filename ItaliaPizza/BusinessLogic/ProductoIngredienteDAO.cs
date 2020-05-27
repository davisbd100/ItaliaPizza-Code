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

   public class ProductoIngredienteDAO : IProductoIngrediente
    {
        public ResultadoOperacionEnum.ResultadoOperacion AddProductoIngrediente(ProductoIngrediente productoIngrediente, Inventario inventario)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarProductoIngrediente");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.Producto VALUES (@Codigo, @Nombre, @Descripcion, @Restriccion)";
                    command.Parameters.Add(new SqlParameter("@Codigo", productoIngrediente.Código));
                    command.Parameters.Add(new SqlParameter("@Nombre", productoIngrediente.Nombre));
                    command.Parameters.Add(new SqlParameter("@Descripcion", productoIngrediente.Descripción));
                    command.Parameters.Add(new SqlParameter("@Restriccion", productoIngrediente.Restricción));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.ProductoIngrediente VALUES (@idProductoIngrediente, @TipoIngrediente)";
                    command.Parameters.Add(new SqlParameter("@idProductoIngrediente", productoIngrediente.Código));
                    command.Parameters.Add(new SqlParameter("@TipoIngrediente", productoIngrediente.tipoIngrediente));
                    command.ExecuteNonQuery();

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
                        "INSERT INTO dbo.Inventario VALUES (@idInventario, @ExistenciaTotal, @UnidadMedida)";
                    command.Parameters.Add(new SqlParameter("@idInventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@ExistenciaTotal", inventario.ExistenciaTotal));
                    command.Parameters.Add(new SqlParameter("@UnidadMedida", inventario.UnidadDeMedida));
                    command.ExecuteNonQuery();

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

        public ResultadoOperacionEnum.ResultadoOperacion EditarProducto(ProductoIngrediente productoIngrediente)
        {

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE dbo.ProductoIngrediente SET Nombre = @Nombre, Descripcion = @Descripcion, " +
                    "Restriccion = @Restriccion, TipoIngrediente = @TipoIngrediente WHERE idProductoIngrediente = @idProductoIngrediente) ", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idProductoIngrediente", productoIngrediente.Código));
                    command.Parameters.Add(new SqlParameter("@Nombre", productoIngrediente.Nombre));
                    command.Parameters.Add(new SqlParameter("@Descripcion", productoIngrediente.Descripción));
                    command.Parameters.Add(new SqlParameter("@Restriccion", productoIngrediente.Restricción));
                    command.Parameters.Add(new SqlParameter("@TipoIngrediente", productoIngrediente.tipoIngrediente.ToString()));

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                    }
                    catch (SqlException)
                    {
                        resultado = ResultadoOperacion.FalloSQL;
                        return resultado;
                    }

                }
                resultado = ResultadoOperacion.Exito;

            }
            return resultado;
        }

        public ResultadoOperacionEnum.ResultadoOperacion EliminarProducto(ProductoIngrediente productoIngrediente)
        {

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE dbo.ProductoIngrediente SET Visibilidad = Invisible WHERE idProductoIngrediente = @idProductoIngrediente) ", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idProductoIngrediente", productoIngrediente.Código));

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                    }
                    catch (SqlException)
                    {
                        resultado = ResultadoOperacion.FalloSQL;
                        return resultado;
                    }
                }
                resultado = ResultadoOperacion.Exito;

            }
            return resultado;
        }


        public List<ProductoIngrediente> GetProductosIngrediente(int rango)
        {
            List<ProductoIngrediente> listaProductos = new List<ProductoIngrediente>();
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
                using (SqlCommand command = new SqlCommand("select Codigo, Nombre  from dbo.ProductoIngrediente left join dbo.Producto  " +
                    "on dbo.Producto.Codigo = dbo.ProductoIngrediente.idProductoIngrediente order by Nombre offset @Rango rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoIngrediente productoIngrediente = new ProductoIngrediente();
                        productoIngrediente.Código = Convert.ToInt32( reader["Codigo"].ToString());
                        productoIngrediente.Nombre = reader["Nombre"].ToString();

                        listaProductos.Add(productoIngrediente);
                    }
                }
                connection.Close();
            }
            return listaProductos;

        }

        public ProductoIngrediente ObtenerProductoIngredientePorId(string codigo)
        {
            ProductoIngrediente productoIngrediente = new ProductoIngrediente();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoIngrediente WHERE idProductoIngrediente = @Codigo", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", codigo));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productoIngrediente.Código = Convert.ToInt32( reader["Codigo"].ToString());
                        productoIngrediente.Descripción = reader["Descripcion"].ToString();
                        productoIngrediente.Nombre = reader["Nombre"].ToString();
                        productoIngrediente.Restricción = reader["Restriccion"].ToString();

                        productoIngrediente.tipoIngrediente = (TipoIngredienteEnum)Enum.Parse(typeof(TipoIngredienteEnum), reader["TipoProducto"].ToString());
                    }
                }
                connection.Close();
            }
            return productoIngrediente;
        }

        public List<ProductoIngrediente> ProdctoIngredienteBusqueda(string busqueda)
        {
            List<ProductoIngrediente> listaProductos = new List<ProductoIngrediente>();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoIngrediente WHERE Nombre LIKE @Busqueda", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Busqueda", busqueda));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoIngrediente productoIngrediente = new ProductoIngrediente();
                        productoIngrediente.Código = Convert.ToInt32( reader["Codigo"].ToString());
                        productoIngrediente.Nombre = reader["Nombre"].ToString();

                        listaProductos.Add(productoIngrediente);
                    }
                }
                connection.Close();
            }
            return listaProductos;
        }
    }
}
