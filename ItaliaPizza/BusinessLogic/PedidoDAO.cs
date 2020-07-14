using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data.Entity.Core;
using System.Data.Entity;
using System.Management.Instrumentation;

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
                }
                catch (EntityException)
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

        public ResultadoOperacionEnum.ResultadoOperacion CambiarProductosDePedido(int pedido, ICollection<DataAccess.PedidoProducto> productos)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            using (var context = new PizzaEntities())
            {
                try
                {
                    var tempPedido = context.Pedido.Where(b => b.idPedido == pedido).FirstOrDefault();
                    for (int i = 0; i < tempPedido.PedidoProducto.Count; i++)
                    {
                        context.PedidoProducto.Remove(context.PedidoProducto.Where(b => b.idPedido == tempPedido.idPedido).First()); //Buscar otra forma de hacerlo
                    }
                    foreach (var producto in productos)
                    {
                        PedidoProducto tempPedidoProducto = new PedidoProducto()
                        {
                            Precio = producto.Precio,
                            idPedido = producto.idPedido,
                            Cantidad = producto.Cantidad,
                            idProductoVenta = producto.ProductoVenta.idProductoVenta
                        };
                        context.PedidoProducto.Add(tempPedidoProducto);
                    }
                    context.SaveChanges();

                    resultado = ResultadoOperacionEnum.ResultadoOperacion.Exito;
                }
                catch (EntityException)
                {
                    resultado = ResultadoOperacionEnum.ResultadoOperacion.FalloSQL;
                }
            }
            return resultado;
        }

        public DataAccess.Pedido GetPedidoConProductoPorId(int id)
        {
            DataAccess.Pedido pedido = new DataAccess.Pedido();
            using (var context = new PizzaEntities())
            {
                try
                {
                    pedido = context.Pedido.Where(b => b.idPedido == id).FirstOrDefault();
                    if (pedido == null)
                    {
                        throw new InstanceNotFoundException();
                    }
                    pedido.PedidoProducto = pedido.PedidoProducto;
                    pedido.Estatus1 = pedido.Estatus1;
                    foreach (var pedidoProducto in pedido.PedidoProducto)
                    {
                        pedidoProducto.ProductoVenta = pedidoProducto.ProductoVenta;
                    }
                }
                catch (EntityException)
                {
                    throw new EntityException("Error al conectar a la bd");
                }catch (InstanceNotFoundException)
                {
                    throw new InstanceNotFoundException("No se encontro el pedido");
                }
            }
            return pedido;
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
        public List<DataAccess.PedidoProducto> ObtenerListaPedidoProducto(int id)
        {
            List<DataAccess.PedidoProducto> pedidos = new List<DataAccess.PedidoProducto>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    pedidos = context.PedidoProducto.Where(b => b.idPedido == id).ToList();
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }
        public List<DataAccess.Pedido> ObtenerListaPedidosDisponiblesCocina()
        {
            List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    DataAccess.Estatus estatusCancelado = context.Estatus.Where(b => b.NombreEstatus == "Cancelado").FirstOrDefault();
                    DataAccess.Estatus estatusFinalizado = context.Estatus.Where(b => b.NombreEstatus == "Finalizado").FirstOrDefault();
                    pedidos = context.Pedido.Where(b => b.Estatus != estatusCancelado.idEstatus && b.Estatus != estatusFinalizado.idEstatus).ToList();
                    foreach (var pedido in pedidos)
                    {
                        pedido.PedidoProducto = pedido.PedidoProducto;
                    }
                }
                catch (EntityException e)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }
        public List<DataAccess.Pedido> ObtenerListaPedidosDisponibles()
        {
            List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    DataAccess.Estatus estatusEnEspera = context.Estatus.Where(b => b.NombreEstatus == "Cancelado").FirstOrDefault();
                    DataAccess.Estatus estatusEnPreparacion = context.Estatus.Where(b => b.NombreEstatus == "Finalizado").FirstOrDefault();
                    pedidos = context.Pedido.Where(b => b.Estatus == estatusEnEspera.idEstatus || b.Estatus == estatusEnPreparacion.idEstatus).ToList();
                    foreach (var pedido in pedidos)
                    {
                        pedido.PedidoProducto = pedido.PedidoProducto;
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }

        public int ObtenerPaginasDeTablaPedido(int elementosPorPagina)
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
                using (SqlCommand command = new SqlCommand("SELECT CEILING((COUNT(*) / @elementos)) AS total FROM dbo.Pedido", connection))
                {
                    command.Parameters.Add(new SqlParameter("@elementos", (float)elementosPorPagina));
                    paginas = (int)command.ExecuteScalar();
                }
                connection.Close();
            }
            return paginas;
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
                        if (pedido.FechaPedido >= inicial && pedido.FechaPedido <= final)
                        {
                            pedido.PedidoProducto = pedido.PedidoProducto;
                            foreach (var pedidoProducto in pedido.PedidoProducto)
                            {
                                pedidoProducto.ProductoVenta = pedidoProducto.ProductoVenta;
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

        public List<DataAccess.Pedido> ObtenerPedidosPorRangoCocinero(int rango, int pagina)
        {
            List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    pedidos = context.Pedido
                        .Where(b => b.Estatus1.NombreEstatus == "En espera" || b.Estatus1.NombreEstatus == "En preparación")
                        .Skip(rango * pagina)
                        .Take(rango)
                        .ToList();
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }

        public List<DataAccess.Pedido> ObtenerPedidosPorRangoCocinero(int rango)
        {
            throw new NotImplementedException();
        }
    }
}
