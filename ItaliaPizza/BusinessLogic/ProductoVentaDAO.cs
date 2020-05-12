using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DatabaseConnection;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class ProductoVentaDAO : IProductoVenta
    {
        public ResultadoOperacionEnum.ResultadoOperacion AddProductoVenta(ProductoVenta productoVenta)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;


            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarProductoVenta");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.Producto VALUES (@Codigo, @Nombre, @Descripcion, @Restriccion)";
                    command.Parameters.Add(new SqlParameter("@Codigo", productoVenta.Código));
                    command.Parameters.Add(new SqlParameter("@Nombre", productoVenta.Nombre));
                    command.Parameters.Add(new SqlParameter("@Descripcion", productoVenta.Descripción));
                    command.Parameters.Add(new SqlParameter("@Restriccion", productoVenta.Restricción));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.ProductoVenta VALUES (@idProductoVenta, @PrecioPublico," +
                        " @TipoProducto, @FotoProducto, @TieneReceta, @Receta)";
                    command.Parameters.Add(new SqlParameter("@idProductoVenta", productoVenta.Código));
                    command.Parameters.Add(new SqlParameter("@PrecioPublico", productoVenta.PrecioPúblico));
                    command.Parameters.Add(new SqlParameter("@TipoProducto", productoVenta.TipoProducto));
                    command.Parameters.Add(new SqlParameter("@Fotoproducto", productoVenta.FotoProducto));
                    command.Parameters.Add(new SqlParameter("@TieneReceta", productoVenta.TieneReceta));
                    command.Parameters.Add(new SqlParameter("@Receta", productoVenta.Receta.id));

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



        public ResultadoOperacionEnum.ResultadoOperacion EditarProductoVenta(ProductoVenta productoVenta)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacionEnum.ResultadoOperacion EliminarProductoVenta(ProductoVenta productoVenta)
        {
            throw new NotImplementedException();
        }


        public List<ProductoVenta> GetProductosVenta(int rango)
        {
            List<ProductoVenta> listaProductos = new List<ProductoVenta>();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoVenta ORDER BY Nombre LIMIT 20 OFFSET @Rango", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoVenta productoVenta = new ProductoVenta();
                        productoVenta.Código = reader["Codigo"].ToString();
                        productoVenta.Nombre = reader["Nombre"].ToString();

                        listaProductos.Add(productoVenta);
                    }
                }
                connection.Close();
            }
            return listaProductos;

        }


        public ProductoVenta ObtenerProductoVentaPorid(string codigo)
        {

            ProductoVenta productoVenta = new ProductoVenta();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoVenta WHERE idProductoVenta = @Codigo", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", codigo));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productoVenta.Código= reader["Codigo"].ToString();
                        productoVenta.Descripción = reader["Descripcion"].ToString();
                        productoVenta.Nombre = reader["Nombre"].ToString();
                        productoVenta.Restricción = reader["Restriccion"].ToString();

                        productoVenta.PrecioPúblico = float.Parse(reader["PrecioPublico"].ToString());
                        productoVenta.TipoProducto = (TipoProductoEnum)Enum.Parse(typeof(TipoProductoEnum) , reader["TipoProducto"].ToString());
                        productoVenta.FotoProducto = reader["FotoProducto"].ToString();
                    }
                }
                connection.Close();
            }
            return productoVenta;
        }

        public List<ProductoVenta> ProductoVentaBusqueda(string busqueda)
        {
            throw new NotImplementedException();
        }
    }
}
