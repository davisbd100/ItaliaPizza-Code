using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    class PedidoDAO : IPedido
    {
        public ResultadoOperacionEnum.ResultadoOperacion CambiarEstadoPedido(Pedido pedido, Estatus estatus)
        {
            ResultadoOperacionEnum.ResultadoOperacion result = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;

            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    result = ResultadoOperacionEnum.ResultadoOperacion.FalloSQL;
                    throw (ex);
                }
                using (SqlCommand command = new SqlCommand("UPDATE Pedido SET Estatus = @estatus WHERE idPedido = @id ", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", pedido.idPedido));
                    command.Parameters.Add(new SqlParameter("@estatus", pedido.Estatus));
                    command.ExecuteNonQuery();
                    result = ResultadoOperacionEnum.ResultadoOperacion.Exito;
                }
                connection.Close();
            }
            return result;
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