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
                    command.Parameters.Add(new SqlParameter("@idProdcutoVenta", productoVenta.Código));
                    command.Parameters.Add(new SqlParameter("@PrecioPublico", productoVenta.PrecioPúblico));
                    command.Parameters.Add(new SqlParameter("@TipoProducto", productoVenta.TipoProducto));
                    command.Parameters.Add(new SqlParameter("@Fotoproducto", productoVenta.FotoProducto));
                    command.Parameters.Add(new SqlParameter("@TieneReceta", productoVenta.TieneReceta));
                    command.Parameters.Add(new SqlParameter("@Receta", productoVenta.Receta));

                    transaction.Commit();
                    resultado = ResultadoOperacion.Exito;

                }
                catch(SqlException e)
                {
                    transaction.Rollback();

                    switch (e.Number)
                    {
                        case 2601:
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

        public List<ProductoVenta> GetProductosVenta()
        {
            throw new NotImplementedException();
        }

        public ProductoVenta ObtenerProductoVentaPorid(string id)
        {
            throw new NotImplementedException();
        }

        public List<ProductoVenta> ProductoVentaBusqueda(string busqueda)
        {
            throw new NotImplementedException();
        }
    }
}
