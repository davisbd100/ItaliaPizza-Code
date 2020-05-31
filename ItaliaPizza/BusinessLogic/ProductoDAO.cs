using System;
using DatabaseConnection;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class ProductoDAO : IProducto
    {
        public List<Producto> BuscarProducto(string busqueda)
        {
            List<Producto> listaProductos = new List<Producto>();
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
                        Producto productoVenta = new Producto();
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

        public List<Producto> getProductos(int rango)
        {
            List<Producto> listaProductos = new List<Producto>();

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
                using (SqlCommand command = new SqlCommand("select *  from dbo.Producto " +
                    "left join dbo.ProductoVenta  on dbo.Producto.idProducto = dbo.ProductoVenta.idProductoVenta " +
                    "left join dbo.ProductoIngrediente  on dbo.Producto.idProducto = dbo.ProductoIngrediente.idProductoIngrediente" +
                    " WHERE dbo.Producto.Visibilidad = 'TRUE' order by Nombre offset 1 rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.idProducto = Convert.ToInt32(reader["idProducto"].ToString());
                        producto.Código = reader["Codigo"].ToString();
                        producto.Nombre = reader["Nombre"].ToString();
                        producto.Descripción = reader["Descripcion"].ToString();
                        
                        
                        listaProductos.Add(producto);
                    }
                }
                connection.Close();
            }
            return listaProductos;
        }

    }
}
