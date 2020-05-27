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
                using (SqlCommand command = new SqlCommand("select Codigo, Nombre  from dbo.Producto left join dbo.ProductoVenta  on dbo.Producto.Codigo =" +
                    " dbo.ProductoVenta.idProductoVenta " +
                    "left join dbo.ProductoIngrediente  on dbo.Producto.Codigo =" +
                    " dbo.ProductoIngrediente.idProductoIngrediente order by Nombre offset @Rango rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Código = Convert.ToInt32( reader["Codigo"].ToString());
                        producto.Nombre = reader["Nombre"].ToString();
                        
                        
                        listaProductos.Add(producto);
                    }
                }
                connection.Close();
            }
            return listaProductos;
        }

    }
}
