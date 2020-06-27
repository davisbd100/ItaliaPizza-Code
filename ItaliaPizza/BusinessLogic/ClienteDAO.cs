using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class ClienteDAO : ICliente
    {
        public String PassHash(String data)
        {
            SHA1 sha = SHA1.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder stringBuilderValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                stringBuilderValue.Append(hashData[i].ToString());
            }
            return stringBuilderValue.ToString();
        }

        public ResultadoOperacion AgregarCliente(Cliente cliente)
        {
            const int VALORES_DUPLICADOS = 2601;

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarCliente");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.Persona VALUES (@idPersona, @Nombre, @Apellido, @Telefono, @Email, @Calle, @Numero, @CodigoPostal, @Colonia, @Ciudad, 'TRUE')";
                    command.Parameters.Add(new SqlParameter("@idPersona", cliente.idPersona));
                    command.Parameters.Add(new SqlParameter("@Nombre", cliente.Nombre));
                    command.Parameters.Add(new SqlParameter("@Apellido", cliente.Apellido));
                    command.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono));
                    command.Parameters.Add(new SqlParameter("@Email", cliente.Email));
                    command.Parameters.Add(new SqlParameter("@Calle", cliente.Calle));
                    command.Parameters.Add(new SqlParameter("@Numero", cliente.Numero));
                    command.Parameters.Add(new SqlParameter("@CodigoPostal", cliente.CodigoPostal));
                    command.Parameters.Add(new SqlParameter("@Colonia", cliente.Colonia));
                    command.Parameters.Add(new SqlParameter("@Ciudad", cliente.Ciudad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.Cliente VALUES (@idCliente, 'TRUE')";
                    command.Parameters.Add(new SqlParameter("@idCliente", cliente.idCliente));
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

        public Cliente GetClineteByIdCliente(String idCliente)
        {
            Cliente cliente = new Cliente();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Cliente WHERE idCliente = @idClienteToSearch", connection))
                {
                    command.Parameters.Add(new SqlParameter("idClienteToSearch", idCliente));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cliente.idCliente = reader["idCliente"].ToString();
                    }
                }
                connection.Close();
            }
            return cliente;
        }

        public List<Cliente> BuscarCliente(string busqueda)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> BuscarClienteDireccion(string busqueda)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> BuscarClienteTelefono(string busqueda)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacion EditarCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacion EliminarCliente(string idCliente)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("EliminarCliente");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "UPDATE dbo.Persona SET VISIBILIDAD = 'FALSE' WHERE idPersona = @idPersona";
                    command.Parameters.Add(new SqlParameter("@idPersona", idCliente));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "UPDATE dbo.Cliente SET VISIBILIDAD = 'FALSE' WHERE idCliente = @idCliente";
                    command.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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

        public List<Cliente> GetCliente(int rango)
        {
            List<Cliente> listaClientes = new List<Cliente>();
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
                using (SqlCommand command = new SqlCommand("SELECT idPersona, Nombre, Apellido, Telefono, Email, Calle, " +
                    "Numero, CodigoPostal, Colonia, Ciudad FROM dbo.Persona left join dbo.Cliente ON dbo.Persona.idPersona = " +
                    "dbo.Cliente.idCliente  WHERE dbo.Persona.Visibilidad = 'TRUE' and idPersona = idCliente order by Nombre offset @Rango " +
                    "rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.idPersona = reader["idPersona"].ToString();
                        cliente.Nombre = reader["Nombre"].ToString();
                        cliente.Apellido = reader["Apellido"].ToString();
                        cliente.Telefono = reader["Telefono"].ToString();
                        cliente.Email = reader["Email"].ToString();
                        cliente.Calle = reader["Calle"].ToString();
                        cliente.Numero = reader["Numero"].ToString();
                        cliente.CodigoPostal = reader["CodigoPostal"].ToString();
                        cliente.Colonia = reader["Colonia"].ToString();
                        cliente.Ciudad = reader["Ciudad"].ToString();
                        listaClientes.Add(cliente);
                    }
                }
                connection.Close();
            }
            return listaClientes;
        }
    }
}
