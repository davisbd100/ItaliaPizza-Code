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
        public ResultadoOperacionEnum.ResultadoOperacion AddProductoVenta(ProductoVenta productoVenta, Inventario inventario)
        {
            const int VALORES_DUPLICADOS = 2601;
            const int VALOR_EXISTENTE = 2627;
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
                           "INSERT INTO dbo.Producto output INSERTED.idProducto VALUES ( @Codigo, @Nombre, @Descripcion, @Restriccion, 'TRUE')";
                    //  command.Parameters.Add(new SqlParameter("@idProducto", productoIngrediente.idProducto));
                    command.Parameters.Add(new SqlParameter("@Codigo", productoVenta.Código));
                    command.Parameters.Add(new SqlParameter("@Nombre", productoVenta.Nombre));
                    command.Parameters.Add(new SqlParameter("@Descripcion", productoVenta.Descripción));
                    command.Parameters.Add(new SqlParameter("@Restriccion", productoVenta.Restricción));
                    //command.ExecuteNonQuery();
                    int id = (int)command.ExecuteScalar();

                    command.CommandText =
                        "INSERT INTO dbo.ProductoVenta VALUES (@idProductoVenta, @PrecioPublico," +
                        " @TipoProducto, @FotoProducto, @TieneReceta, @Receta, 'TRUE')";
                    command.Parameters.Add(new SqlParameter("@idProductoVenta", id));
                    command.Parameters.Add(new SqlParameter("@PrecioPublico", productoVenta.PrecioPúblico));
                    command.Parameters.Add(new SqlParameter("@TipoProducto", productoVenta.TipoProducto.IdTipoProducto));
                    command.Parameters.Add(new SqlParameter("@Fotoproducto", productoVenta.FotoProducto));
                    command.Parameters.Add(new SqlParameter("@TieneReceta", productoVenta.TieneReceta));
                    command.Parameters.Add(new SqlParameter("@Receta", productoVenta.Receta.IdReceta));
                    command.ExecuteNonQuery();

                    command.CommandText =
                         "INSERT INTO dbo.ProductoInventario VALUES (@Inventario, @Producto, @CantidadIngreso, @PrecioCompra, @FechaIngreso, @Caducidad, 'TRUE')";
                    command.Parameters.Add(new SqlParameter("@Inventario", id));
                    command.Parameters.Add(new SqlParameter("@Producto", id));
                    command.Parameters.Add(new SqlParameter("@CantidadIngreso", inventario.CantidadIngreso));
                    command.Parameters.Add(new SqlParameter("@PrecioCompra", inventario.PrecioCompra));
                    command.Parameters.Add(new SqlParameter("@FechaIngreso", inventario.FechaIngreso));
                    command.Parameters.Add(new SqlParameter("@Caducidad", inventario.Caducidad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.Inventario VALUES (@idInventario, @ExistenciaTotal, @UnidadMedida, 'TRUE')";
                    command.Parameters.Add(new SqlParameter("@idInventario", id));
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
                        case VALOR_EXISTENTE:
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
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE dbo.Productoventa SET Nombre = @Nombre, Descripcion = @Descripcion, " +
                    "Restriccion = @Restriccion, TipoProducto = @TipoProducto, PrecioPublico = @PrecioPublico, " +
                    "FotoProducto = @FotoProducto, TieneReceta = @TieneReceta, Receta = @Receta WHERE idProductoVenta = @idProductoVenta ", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idProductoVenta", productoVenta.Código));
                    command.Parameters.Add(new SqlParameter("@Nombre", productoVenta.Nombre));
                    command.Parameters.Add(new SqlParameter("@Descripcion", productoVenta.Descripción));
                    command.Parameters.Add(new SqlParameter("@Restriccion", productoVenta.Restricción));
                    command.Parameters.Add(new SqlParameter("@TipoProducto", productoVenta.TipoProducto.IdTipoProducto.ToString()));
                    command.Parameters.Add(new SqlParameter("@PrecioPublico", productoVenta.PrecioPúblico));
                    command.Parameters.Add(new SqlParameter("@FotoProducto", productoVenta.FotoProducto));
                    command.Parameters.Add(new SqlParameter("@TieneReceta", productoVenta.TieneReceta));
                    command.Parameters.Add(new SqlParameter("@Receta", productoVenta.Receta.IdReceta));
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

        public ResultadoOperacionEnum.ResultadoOperacion EliminarProductoVenta(int productoVenta)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("Eliminar producto");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "UPDATE dbo.Producto  SET VISIBILIDAD = 'FALSE' WHERE idProducto = @Codigo";
                    command.Parameters.Add(new SqlParameter("@Codigo", productoVenta));

                    command.ExecuteNonQuery();

                    command.CommandText =
                        "UPDATE dbo.ProductoVenta SET VISIBILIDAD = 'FALSE'  WHERE idProductoVenta =  @idProductoVenta";
                    command.Parameters.Add(new SqlParameter("@idProductoVenta", productoVenta));


                    command.ExecuteNonQuery();

                    command.CommandText =
                         "UPDATE dbo.ProductoInventario SET VISIBILIDAD = 'FALSE' WHERE Producto = @Producto ";
                    command.Parameters.Add(new SqlParameter("@Producto", productoVenta));

                    command.ExecuteNonQuery();

                    command.CommandText =
                        "UPDATE dbo.Inventario SET VISIBILIDAD = 'FALSE'  WHERE idInventario =@idInventario";
                    command.Parameters.Add(new SqlParameter("@idInventario", productoVenta));


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

                using (SqlCommand command = new SqlCommand("select Codigo, Nombre, Descripcion, idProducto  from dbo.ProductoVenta left join dbo.Producto  on" +
                    " dbo.Producto.idProducto = dbo.ProductoVenta.idProductoVenta WHERE dbo.Producto.Visibilidad = 'TRUE' order by Nombre offset @Rango rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoVenta productoVenta = new ProductoVenta();
                        productoVenta.idProducto = Convert.ToInt32(reader["idProducto"].ToString());
                        productoVenta.Código = reader["Codigo"].ToString();
                        productoVenta.Nombre = reader["Nombre"].ToString();
                        productoVenta.PrecioPúblico = float.Parse(reader["PrecioPublico"].ToString());
                        productoVenta.Descripción = reader["Descripcion"].ToString();

                        listaProductos.Add(productoVenta);
                    }
                }
                connection.Close();
            }
            return listaProductos;

        }

        public int ObtenerPaginasDeTablaProductoVenta()
        {
            int paginas = 0;

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
                using (SqlCommand command = new SqlCommand("SELECT CEILING((COUNT(*) / @elementos)) AS total FROM dbo.ProductoVenta", connection))
                {
                    command.Parameters.Add(new SqlParameter("@elementos", (float)20));
                    paginas = int.Parse(command.ExecuteScalar().ToString());
                }
                connection.Close();
            }
            return paginas;
        }



        public List<ProductoVenta> GetProductosVentaSinRecetaAsignada(int rango)
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
                using (SqlCommand command = new SqlCommand("select Codigo, Nombre, Descripcion, idProducto  from dbo.ProductoVenta left join dbo.Producto  on" +
                    " dbo.Producto.idProducto = dbo.ProductoVenta.idProductoVenta WHERE dbo.Producto.Visibilidad = 'TRUE' and dbo.productoVenta.TieneReceta = 1 and dbo.productoVenta.Receta = 0 order by Nombre offset @Rango rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoVenta productoVenta = new ProductoVenta();
                        productoVenta.idProducto = Convert.ToInt32(reader["idProducto"].ToString());
                        productoVenta.Código = reader["Codigo"].ToString();
                        productoVenta.Nombre = reader["Nombre"].ToString();
                        productoVenta.Descripción = reader["Descripcion"].ToString();

                        listaProductos.Add(productoVenta);
                    }
                }
                connection.Close();
            }
            return listaProductos;

        }


        public ProductoVenta ObtenerProductoVentaPorid(int codigo)
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoVenta left join dbo.Producto on " +
                    " dbo.Producto.idProducto = dbo.ProductoVenta.idProductoVenta WHERE idProductoVenta = @Codigo", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", codigo));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productoVenta.idProducto = Convert.ToInt32(reader["idProducto"].ToString());
                        productoVenta.Código = reader["Codigo"].ToString();
                        productoVenta.Descripción = reader["Descripcion"].ToString();
                        productoVenta.Nombre = reader["Nombre"].ToString();
                        productoVenta.Restricción = reader["Restriccion"].ToString();

                        productoVenta.PrecioPúblico = float.Parse(reader["PrecioPublico"].ToString());
                        //productoVenta.TipoProducto.IdTipoProducto = int.Parse(reader["TipoProducto"].ToString());
                        productoVenta.FotoProducto = reader["FotoProducto"].ToString();
                    }
                }
                connection.Close();
            }
            return productoVenta;
        }

        public List<ProductoVenta> ProductoVentaBusqueda(string busqueda)
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Producto WHERE Nombre LIKE @Busqueda", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Busqueda", busqueda));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoVenta productoVenta = new ProductoVenta();
                        productoVenta.idProducto = Convert.ToInt32(reader["idProducto"].ToString());
                        productoVenta.Código = reader["Codigo"].ToString();
                        productoVenta.Nombre = reader["Nombre"].ToString();

                        listaProductos.Add(productoVenta);
                    }
                }
                connection.Close();
            }
            return listaProductos;
        }

        public List<ProductoVenta> ProductoVentaBusquedaRango(int rango, string busqueda)
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
                using (SqlCommand command = new SqlCommand("select Codigo, Nombre, PrecioPublico from dbo.ProductoVenta left join dbo.Producto  on" +
                    " dbo.Producto.Codigo = dbo.ProductoVenta.idProductoVenta WHERE Nombre LIKE @Busqueda order by Nombre offset @Rango rows fetch next 20 rows only ", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    command.Parameters.Add(new SqlParameter("@Busqueda", busqueda));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoVenta productoVenta = new ProductoVenta();
                        productoVenta.Código = Convert.ToInt32(reader["Codigo"].ToString());
                        productoVenta.Nombre = reader["Nombre"].ToString();
                        productoVenta.PrecioPúblico = float.Parse(reader["PrecioPublico"].ToString());

                        listaProductos.Add(productoVenta);
                    }
                }
                connection.Close();
            }
            return listaProductos;
        }
    }
}
