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
using static BusinessLogic.ResultadoOperacionEnum;
using System.CodeDom.Compiler;

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
        public ResultadoOperacionEnum.ResultadoOperacion AsignarEntrega(int id, String Repartidor)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = new ResultadoOperacionEnum.ResultadoOperacion();
            using (var context = new PizzaEntities())
            {
                try
                {
                    int Estatus = context.Estatus.Where(b => b.NombreEstatus == "En Camino").FirstOrDefault().idEstatus;

                    var tempPedido = context.Pedido
                                    .Where(b => b.idPedido == id)
                                    .FirstOrDefault();
                    var tempPedidoDomicilio = context.PedidoDomicilio.Where(b => b.idPedido == id).FirstOrDefault();
                    
                    
                    tempPedidoDomicilio.HoraSalida = DateTime.Now;
                    tempPedidoDomicilio.Repartidor = Repartidor;
                    tempPedido.Estatus = Estatus;

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
        public ResultadoOperacionEnum.ResultadoOperacion PonerEnPreparacion(int id)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = new ResultadoOperacionEnum.ResultadoOperacion();
            using (var context = new PizzaEntities())
            {
                try
                {
                    int Estatus = context.Estatus.Where(b => b.NombreEstatus == "En Preparación").FirstOrDefault().idEstatus;

                    var tempPedido = context.Pedido
                                    .Where(b => b.idPedido == id)
                                    .FirstOrDefault();
                    foreach (var item in tempPedido.PedidoProducto)
                    {
                        var tempInventario = context.Inventario.Where(b => b.Producto == item.idProductoVenta).FirstOrDefault();
                        if (tempInventario.ExistenciaTotal < item.Cantidad)
                        {
                            throw new ArgumentException("El inventario no cuenta con la cantidad para despachar este pedido");
                        }
                        else
                        {
                            tempInventario.ExistenciaTotal -= item.Cantidad;
                        }
                    }

                    tempPedido.Estatus = Estatus;

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
        public bool EsADomicilio(int id)
        {
            bool resultado = false;
            using (var context = new PizzaEntities())
            {
                try
                {
                    var tempPedidoDomicilio = context.PedidoDomicilio.Where(b => b.idPedido == id).FirstOrDefault();
                    if (tempPedidoDomicilio != null)
                    {
                        resultado = true;
                    }
                }
                catch (EntityException)
                {
                    throw new EntityException("Error no identificado");
                }
                return resultado;
            }
        }

        public ResultadoOperacionEnum.ResultadoOperacion CambiarProductosDePedido(int pedido, List<DataAccess.PedidoProducto> productos)
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

        public ResultadoOperacionEnum.ResultadoOperacion CrearPedidoDomicilio(DataAccess.Pedido pedido, List<PedidoProducto> productos)

        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;

            Random random = new Random();
            pedido.idPedido = random.Next(1000);


            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarPedido");
                command.Connection = connection;
                command.Transaction = transaction;
                
                try 
                {
                    command.CommandText =
                         "INSERT INTO dbo.Pedido (idPedido, FechaPedido, Estatus, Cliente) SELECT @idPedido, @FechaPedido, @Estatus, @Cliente";

                    command.Parameters.Add(new SqlParameter("@idPedido", pedido.idPedido));
                    command.Parameters.Add(new SqlParameter("@FechaPedido", pedido.FechaPedido));
                    //command.Parameters.Add(new SqlParameter("@NumeroMesa", 1));
                    command.Parameters.Add(new SqlParameter("@Estatus", pedido.Estatus));
                    command.Parameters.Add(new SqlParameter("@Cliente", pedido.Cliente));
                    //command.Parameters.Add(new SqlParameter("@Empleado", 0));
                    //command.Parameters.Add(new SqlParameter("@DiaVenta", 0));
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.CommandText =
                    "INSERT INTO dbo.PedidoDomicilio VALUES (@idPedido, @Comentario, @HoraSalida, @Repartidor)";
                    command.Parameters.Add(new SqlParameter("@idPedido", pedido.idPedido));
                    command.Parameters.Add(new SqlParameter("@Comentario", ""));
                    command.Parameters.Add(new SqlParameter("@HoraSalida", ""));
                    command.Parameters.Add(new SqlParameter("@Repartidor", ""));
                    command.ExecuteNonQuery();




                    for (int posicion = 0; posicion < productos.Count; posicion++)
                    {
                        command.Parameters.Clear();

                        command.CommandText =
                        "INSERT INTO dbo.PedidoProducto VALUES (@idPedido, @idProductoVenta, @Cantidad, @Precio)";
                        command.Parameters.Add(new SqlParameter("@idPedido", pedido.idPedido));
                        command.Parameters.Add(new SqlParameter("@idProductoVenta", productos[posicion].idProductoVenta));
                        command.Parameters.Add(new SqlParameter("@Cantidad", productos[posicion].Cantidad));
                        command.Parameters.Add(new SqlParameter("@Precio", productos[posicion].Precio));


                        command.ExecuteNonQuery();

                    }




                    transaction.Commit();
                    resultado = ResultadoOperacion.Exito;

                }
                catch (SqlException e)
                {
                    transaction.Rollback();

                    switch (e.Number)
                    {
                        default:
                            resultado = ResultadoOperacion.FalloSQL;
                            break;
                    }
                }
            }



            return resultado;

        }

        public ResultadoOperacion CrearPedidoMesero(DataAccess.Pedido pedido, List<PedidoProducto> productos)
        {

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;

            Random random = new Random();
            pedido.idPedido = random.Next(1000);


            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarPedido");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.Pedido (idPedido, FechaPedido, Estatus, NumeroMesa) SELECT @idPedido, @FechaPedido, @Estatus, @NumeroMesa";

                    command.Parameters.Add(new SqlParameter("@idPedido", pedido.idPedido));
                    command.Parameters.Add(new SqlParameter("@FechaPedido", pedido.FechaPedido));
                    command.Parameters.Add(new SqlParameter("@Estatus", pedido.Estatus));
                    //command.Parameters.Add(new SqlParameter("@Cliente", pedido.Cliente));
                    command.Parameters.Add(new SqlParameter("@NumeroMesa", 1));

                    //command.Parameters.Add(new SqlParameter("@Empleado", 0));
                    //command.Parameters.Add(new SqlParameter("@DiaVenta", 0));
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();


                    for (int posicion = 0; posicion < productos.Count; posicion++)
                    {
                        command.Parameters.Clear();

                        command.CommandText =
                        "INSERT INTO dbo.PedidoProducto VALUES (@idPedido, @idProductoVenta, @Cantidad, @Precio)";
                        command.Parameters.Add(new SqlParameter("@idPedido", pedido.idPedido));
                        command.Parameters.Add(new SqlParameter("@idProductoVenta", productos[posicion].idProductoVenta));
                        command.Parameters.Add(new SqlParameter("@Cantidad", productos[posicion].Cantidad));
                        command.Parameters.Add(new SqlParameter("@Precio", productos[posicion].Precio));


                        command.ExecuteNonQuery();

                    }




                    transaction.Commit();
                    resultado = ResultadoOperacion.Exito;

                }
                catch (SqlException e)
                {
                    transaction.Rollback();

                    switch (e.Number)
                    {
                        default:
                            resultado = ResultadoOperacion.FalloSQL;
                            break;
                    }
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
        public DataAccess.Estatus ObtenerEstatusPorId(int id)
        {
            DataAccess.Estatus resultado = new DataAccess.Estatus();
            using (var context = new PizzaEntities())
            {
                try
                {
                    var tempEstatus = context.Estatus
                                    .Where(b => b.idEstatus == id)
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
                    foreach (var item in context.Pedido)
                    {
                        item.Estatus1 = item.Estatus1;
                        pedidos.Add(item);
                    }
                    
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
                    DataAccess.Estatus estatusEnEspera = context.Estatus.Where(b => b.NombreEstatus == "En Espera").FirstOrDefault();
                    DataAccess.Estatus estatusEnPreparacion = context.Estatus.Where(b => b.NombreEstatus == "En Preparación").FirstOrDefault();
                    pedidos = context.Pedido.Where(b => b.Estatus == estatusEnEspera.idEstatus || b.Estatus == estatusEnPreparacion.idEstatus).ToList();
                    foreach (var pedido in pedidos)
                    {
                        pedido.PedidoProducto = pedido.PedidoProducto;
                        pedido.Estatus1 = pedido.Estatus1;
                    }
                }
                catch (EntityException)
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
                    DataAccess.Estatus estatusCancelado = context.Estatus.Where(b => b.NombreEstatus == "Cancelado").FirstOrDefault();
                    DataAccess.Estatus estatusFinalizado = context.Estatus.Where(b => b.NombreEstatus == "Finalizado").FirstOrDefault();
                    pedidos = context.Pedido.Where(b => b.Estatus != estatusCancelado.idEstatus && b.Estatus != estatusFinalizado.idEstatus).ToList();
                    foreach (var pedido in pedidos)
                    {
                        pedido.PedidoProducto = pedido.PedidoProducto;
                        pedido.Estatus1 = pedido.Estatus1;
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return pedidos;
        }
        public List<DataAccess.Pedido> ObtenerListaPedidosDisponiblesMesero()
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
                        pedido.Estatus1 = pedido.Estatus1;
                    }
                    for (int i = 0; i < pedidos.Count; i++)
                    {
                        if (EsADomicilio(pedidos[i].idPedido))
                        {
                            pedidos.Remove(pedidos[i]);
                            i--;
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
        public List<DataAccess.Pedido> ObtenerListaPedidosCallCenter()
        {
            List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    DataAccess.Estatus estatusCancelado = context.Estatus.Where(b => b.NombreEstatus == "Cancelado").FirstOrDefault();
                    DataAccess.Estatus estatusFinalizado = context.Estatus.Where(b => b.NombreEstatus == "Finalizado").FirstOrDefault();

                    pedidos = context.Pedido.Where(b => b.Estatus != estatusCancelado.idEstatus && b.Estatus != estatusFinalizado.idEstatus && context.PedidoDomicilio.Where(p=> p.idPedido == b.idPedido).FirstOrDefault() != null).ToList();
                    foreach (var pedido in pedidos)
                    {
                        pedido.PedidoProducto = pedido.PedidoProducto;
                        pedido.Estatus1 = pedido.Estatus1;
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
                using (SqlCommand command = new SqlCommand("SELECT CEILING ((COUNT(*) / @elementos)) AS total FROM dbo.Pedido", connection))
                {
                    command.Parameters.Add(new SqlParameter("@elementos", (float)elementosPorPagina));
                    paginas = int.Parse(command.ExecuteScalar().ToString());
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
                            pedido.Estatus1 = pedido.Estatus1;
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
                        .OrderBy(b=> b.FechaPedido)
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
    }
}
