using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data.Entity.Core;

namespace BusinessLogic
{
    class PedidoDAO : IPedido
    {
        public ResultadoOperacionEnum.ResultadoOperacion CambiarEstadoPedido(Pedido pedido, DataAccess.Estatus estatus)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = new ResultadoOperacionEnum.ResultadoOperacion();
            using (var context = new PizzaEntities())
            {
                try
                {
                var tempPedido = context.Pedido
                                .Where(b => b.idPedido == pedido.idPedido)
                                .FirstOrDefault();

                    tempPedido.Estatus1 = estatus;
                    context.SaveChanges();

                    resultado = ResultadoOperacionEnum.ResultadoOperacion.Exito;
                }catch (EntityException)
                {
                    resultado = ResultadoOperacionEnum.ResultadoOperacion.FalloSQL;
                }
                return resultado;
            }
        }

        public ResultadoOperacionEnum.ResultadoOperacion CambiarPedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacionEnum.ResultadoOperacion CambiarProductosDePedido(int pedido, List<ProductoVenta> productos)
        {
            throw new NotImplementedException();
        }

        public Pedido GetPedidoPorId(int id)
        {
            Pedido pedido = new Pedido();

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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Pedido WHERE idPedido = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pedido.Repartidor = reader["Repartidor"].ToString();
                        pedido.FechaPedido = DateTime.Parse(reader["FechaPedido"].ToString());
                        pedido.Comentario = reader["Comentario"].ToString();
                        pedido.idPedido = int.Parse(reader["idPedido"].ToString());
                        pedido.NumeroMesa = int.Parse(reader["NumeroMesa"].ToString());
                    }
                }
                connection.Close();
            }
            return pedido;
        }
    }
}
