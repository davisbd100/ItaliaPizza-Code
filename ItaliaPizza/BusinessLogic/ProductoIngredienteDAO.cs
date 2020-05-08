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
                        "INSERT INTO dbo.ProductoVenta VALUES (@idProductoIngrediente, @TipoIngrediente";
                    command.Parameters.Add(new SqlParameter("@idProductoVenta", productoIngrediente.Código));
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
            throw new NotImplementedException();
        }

        public ResultadoOperacionEnum.ResultadoOperacion EliminarProducto(ProductoIngrediente productoIngrediente)
        {
            throw new NotImplementedException();
        }

        public List<ProductoIngrediente> GetProductosIngrediente()
        {
            throw new NotImplementedException();
        }

        public ProductoIngrediente ObtenerProductoIngredientePorId(string id)
        {
            throw new NotImplementedException();
        }

        public List<ProductoIngrediente> ProdctoIngredienteBusqueda(string busqueda)
        {
            throw new NotImplementedException();
        }
    }
}
