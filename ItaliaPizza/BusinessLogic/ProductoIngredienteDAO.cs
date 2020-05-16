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

    class ProductoIngredienteDAO : IProductoIngrediente
    {
        public ResultadoOperacionEnum.ResultadoOperacion AddProductoIngrediente(ProductoIngrediente productoIngrediente)
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
                        "INSERT INTO dbo.ProductoIngrediente VALUES (@idProductoIngrediente, @TipoIngrediente";
                    command.Parameters.Add(new SqlParameter("@idProductoIngrediente", productoIngrediente.Código));
                    command.Parameters.Add(new SqlParameter("@TipoIngrediente", productoIngrediente.tipoIngrediente));


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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoIngrediente ORDER BY Nombre LIMIT 20 OFFSET @Rango", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoIngrediente productoIngrediente = new ProductoIngrediente();
                        productoIngrediente.Código = reader["Codigo"].ToString();
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
                        productoIngrediente.Código = reader["Codigo"].ToString();
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
                        productoIngrediente.Código = reader["Codigo"].ToString();
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
