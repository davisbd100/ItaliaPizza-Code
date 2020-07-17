using DataAccess;
using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class InventarioDAO : IInventario
    {
        public class CustomProducto : DataAccess.Producto
        {
            public int UnidadesVendidas { get; set; } = 0;
            public double IngresosGenerados { get; set; } = 0;
        }

        public ResultadoOperacionEnum.ResultadoOperacion AddInventario(Inventario inventario)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();


            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("AgregarInventario");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
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

        public ResultadoOperacion ModificarInventario(Inventario inventario)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();


            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("AgregarInventario");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "UPDATE dbo.ProductoInventario SET CantidadIngreso = @CantidadIngreso, PrecioCompra = @PrecioCompra," +
                         "FechaIngreso = @FechaIngreso, Caducidad = @Caducidad WHERE Producto = @Producto AND Inventario = @Inventario";
                    command.Parameters.Add(new SqlParameter("@Inventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@Producto", inventario.Producto.Código));
                    command.Parameters.Add(new SqlParameter("@CantidadIngreso", inventario.CantidadIngreso));
                    command.Parameters.Add(new SqlParameter("@PrecioCompra", inventario.PrecioCompra));
                    command.Parameters.Add(new SqlParameter("@FechaIngreso", inventario.FechaIngreso));
                    command.Parameters.Add(new SqlParameter("@Caducidad", inventario.Caducidad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "UPDATE dbo.Inventario SET ExistenciaTotal =  @ExistenciaTotal, UnidadMedida = Unidadmedida =  @UnidadMedida" +
                        "WHERE idInventario = @idInventario";
                    command.Parameters.Add(new SqlParameter("@idInventario", inventario.idInventario));
                    command.Parameters.Add(new SqlParameter("@ExistenciaTotal", inventario.ExistenciaTotal));
                    command.Parameters.Add(new SqlParameter("@UnidadMedida", inventario.UnidadDeMedida));

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

        public List<Inventario> ObtenerInventarios(Producto producto)
        {
            List<Inventario> inventarios = new List<Inventario>();


            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
 
                    connection.Open();

                
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.ProductoInventario ORDER BY Nombre WHERE Producto = @Producto", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Producto", producto.Código));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inventario inventario = new Inventario();
                        inventario.CantidadIngreso = Convert.ToInt32( reader["CantidadIngreso"]);
                        inventario.PrecioCompra = float.Parse(reader["Precio"].ToString());
                        DateTime fechaIngreso = DateTime.Parse(reader["FechaIngreso"].ToString());
                        inventario.FechaIngreso = fechaIngreso;
                        //  DateTime caducidad = DateTime.Parse(reader["Caducidad"].ToString());
                        // inventario.Caducidad = caducidad;
                        inventario.Caducidad = reader["caducidad"].ToString();
                        inventarios.Add(inventario);
                    }
                }
                connection.Close();
            }

            return inventarios;
        }

        public List<DataAccess.Inventario> ObtenerTodosLosInventarios()
        {
            List<DataAccess.Inventario> inventarios = new List<DataAccess.Inventario>();
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var inventario in context.Inventario)
                    {
                        inventarios.Add(inventario);
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los Inventarios");
                }
            }

            return inventarios;
        }
        public ResultadoOperacion ActualizarInventario(List<DataAccess.Inventario> inventarios)
        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var inventario in context.Inventario)
                    {
                        DataAccess.Inventario tempInventario = inventarios.FirstOrDefault(b => b.idInventario == inventario.idInventario);
                        inventario.ExistenciaInicial = tempInventario.ExistenciaTotal;
                        inventario.ExistenciaTotal = inventario.ExistenciaInicial;
                    }
                    context.SaveChanges();
                }
                catch (EntityException)
                {
                    resultado = ResultadoOperacion.FalloSQL;
                }
            }

            return resultado;
        }

        public List<DataAccess.Inventario> ObtenerTodosLosInventariosConIngreso(int rango, int pagina)
        {
            List<DataAccess.Inventario> inventarios = new List<DataAccess.Inventario>();
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    context.Inventario.FirstOrDefault();
                    foreach (var inventario in (context.Inventario.OrderByDescending(b => b.idInventario).Skip(rango * (pagina - 1)).Take(rango)))
                    {
                        inventarios.Add(inventario);
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los Inventarios");
                }
            }

            return inventarios;
        }
        public List<DataAccess.ProductoInventario> ObtenerProductoInventario(int id)
        {
            List<DataAccess.ProductoInventario> resultado = new List<DataAccess.ProductoInventario>();
            using (var context = new PizzaEntities())
            {
                try
                {
                    resultado = context.ProductoInventario.Where(b => b.Inventario == id).ToList();
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los pedidos");
                }
            }

            return resultado;
        }
        public int ObtenerPaginasDeTablaInventario(int elementosPorPagina)
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
                using (SqlCommand command = new SqlCommand("SELECT CEILING((COUNT(*) / @elementos)) AS total FROM dbo.Inventario", connection))
                {
                    command.Parameters.Add(new SqlParameter("@elementos", (float)elementosPorPagina));
                    paginas = int.Parse(command.ExecuteScalar().ToString());
                }
                connection.Close();
            }
            return paginas;
        }

        public List<DataAccess.Inventario> ObtenerInventarioPorRango(int rango, int pagina)
        {
            List<DataAccess.Inventario> inventarios = new List<DataAccess.Inventario>();
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var inventario in context.Inventario.OrderBy(b => b.idInventario).Skip((pagina - 1) * rango))
                    {
                        inventarios.Add(inventario);
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los Inventarios");
                }
            }

            return inventarios;
        }
        public int ObtenerPaginasDeTablaDiaVenta(int elementosPorPagina)
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
                using (SqlCommand command = new SqlCommand("SELECT CEILING((COUNT(*) / @elementos)) AS total FROM dbo.DiaVenta", connection))
                {
                    command.Parameters.Add(new SqlParameter("@elementos", (float)elementosPorPagina));
                    paginas = int.Parse(command.ExecuteScalar().ToString());
                }
                connection.Close();
            }
            return paginas;
        }

        public List<DataAccess.DiaVenta> ObtenerDiaVentaPorRango(int rango, int pagina)
        {
            List<DataAccess.DiaVenta> DiaVentas = new List<DataAccess.DiaVenta>();
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var DiaVenta in context.DiaVenta.OrderBy(b=> b.idVentaDiaria).Skip((pagina-1)*rango))
                    {
                        DiaVentas.Add(DiaVenta);
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los DiaVentas");
                }
            }

            return DiaVentas;
        }
        public List<CustomProducto> ObtenerProductoVentaDia(int id)
        {
            List<CustomProducto> listaCustomProductos = new List<CustomProducto>();
            List<DataAccess.Producto> productos = new List<DataAccess.Producto>();
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var pedido in context.DiaVenta.Where(b=> b.idVentaDiaria == id).FirstOrDefault().Pedido)
                    {
                        foreach (var pedidoProducto in pedido.PedidoProducto)
                        {
                            if (productos.Contains(context.Producto.Where(b=> b.idProducto == pedidoProducto.idProductoVenta).FirstOrDefault()))
                            {
                                listaCustomProductos.Where(b => b.idProducto == pedidoProducto.idProductoVenta).FirstOrDefault().IngresosGenerados += (double)pedidoProducto.Precio;
                                listaCustomProductos.Where(b => b.idProducto == pedidoProducto.idProductoVenta).FirstOrDefault().UnidadesVendidas += (int)pedidoProducto.Cantidad;
                            }
                            else
                            {
                                DataAccess.Producto tempProducto = context.Producto.Where(b => b.idProducto == pedidoProducto.idProductoVenta).FirstOrDefault();
                                CustomProducto custom = new CustomProducto()
                                {
                                    Codigo = tempProducto.Codigo,
                                    Descripcion = tempProducto.Descripcion,
                                    idProducto = tempProducto.idProducto,
                                    Nombre = tempProducto.Nombre,
                                    Restriccion = tempProducto.Restriccion,
                                    IngresosGenerados = (double)pedidoProducto.Precio,
                                    UnidadesVendidas = (int)pedidoProducto.Cantidad
                                };
                                productos.Add(tempProducto);
                                listaCustomProductos.Add(custom);
                            }
                        }
                    }
                }
                catch (EntityException)
                {
                    throw new Exception("Error al obtener los DiaVentas");
                }
            }

            return listaCustomProductos;
        }
        public ResultadoOperacion CerrarDia()
        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    foreach (var inventario in context.Inventario)
                    {
                        inventario.ExistenciaInicial = inventario.ExistenciaTotal;
                    }
                    context.SaveChanges();
                }
                catch (EntityException)
                {
                    resultado = ResultadoOperacion.FalloSQL;
                }
            }

            return resultado;
        }
    }
}
