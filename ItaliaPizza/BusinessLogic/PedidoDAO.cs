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
    public class PedidoDAO : IPedido
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

                    tempPedido.Estatus = estatus.idEstatus;
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

        public DataAccess.Estatus ObtenerEstatusPorNombre(string estatus)
        {
            DataAccess.Estatus resultado = new DataAccess.Estatus();
            using (var context = new PizzaEntities())
            {
                try
                {
                    var tempEstatus = context.Estatus
                                    .Where(b => b.NombreEstatus == estatus)
                                    .FirstOrDefault();

                    resultado = tempEstatus;
                }
                catch (EntityException)
                {
                    throw new Exception("No se pudo obtener el estatus");
                }
                return resultado;
            }
        }

        public List<DataAccess.Pedido> ObtenerListaPedidos()
        {
            List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    pedidos = context.Pedido.ToList();
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }

        public List<DataAccess.Pedido> ObtenerPedidosPorFecha(DateTime inicial, DateTime final)
        {
            List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    foreach (var pedido in context.Pedido)
                    {
                        if(pedido.FechaPedido >= inicial && pedido.FechaPedido <= final)
                        {
                            pedido.PedidoProducto = pedido.PedidoProducto;
                            foreach (var pedidoProducto in pedido.PedidoProducto)
                            {
                                pedidoProducto.ProductoVenta = pedidoProducto.ProductoVenta;
                                pedidoProducto.ProductoVenta.Receta1 = pedidoProducto.ProductoVenta.Receta1;
                            }
                            pedidos.Add(pedido);
                        }
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }
    }
}
