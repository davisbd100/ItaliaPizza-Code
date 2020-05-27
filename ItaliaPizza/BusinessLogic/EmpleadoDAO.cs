using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseConnection;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class EmpleadoDAO : IEmpleado
    {
        public ResultadoOperacion AgregarEmpleado(Empleado empleado)
        {
            const int VALORES_DUPLICADOS = 2601;
            //string FechaUltimoAcceso = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("InsertarEmpleado");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "INSERT INTO dbo.Persona VALUES (@idPersona, @Nombre, @Apellido, @Telefono, @Email, @Calle, @Numero, @CodigoPostal, @Colonia, @Ciudad)";
                    command.Parameters.Add(new SqlParameter("@idPersona", empleado.idPersona));
                    command.Parameters.Add(new SqlParameter("@Nombre", empleado.Nombre));
                    command.Parameters.Add(new SqlParameter("@Apellido", empleado.Apellido));
                    command.Parameters.Add(new SqlParameter("@Telefono", empleado.Telefono));
                    command.Parameters.Add(new SqlParameter("@Email", empleado.Email));
                    command.Parameters.Add(new SqlParameter("@Calle", empleado.Calle));
                    command.Parameters.Add(new SqlParameter("@Numero", empleado.Numero));
                    command.Parameters.Add(new SqlParameter("@CodigoPostal", empleado.CodigoPostal));
                    command.Parameters.Add(new SqlParameter("@Colonia", empleado.Colonia));
                    command.Parameters.Add(new SqlParameter("@Ciudad", empleado.Ciudad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.Empleado VALUES (@idEmpleado, @TipoEmpleado, @NombreUsuario, @Contrasena, @FechaUltimoAcceso)";
                    command.Parameters.Add(new SqlParameter("@idEmpleado", empleado.idEmpleado));
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", empleado.TipoEmpleado));
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", empleado.NombreUsuario));
                    command.Parameters.Add(new SqlParameter("@Contrasena", empleado.Contraseña));
                    command.Parameters.Add(new SqlParameter("@FechaUltimoAcceso", empleado.FechaUltimoAcceso));
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
                            Console.WriteLine("La tonta fecha es: " + empleado.FechaUltimoAcceso.ToString());
                            Console.WriteLine("El problema es: " + e);
                            break;
                    }
                }
            }

            return resultado;
        }

        public ResultadoOperacion DarDeBajaEmpleado(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacion EditarEmpleado(Empleado empleado)
        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE dbo.Empleado SET Nombre = @Nombre, Apellido = @Apellido, " +
                    "Telefono = @Telefono, Email = @Email, Calle = @Calle, Numero = @Numero, CodigoPostal = @CodigoPostal" +
                    "Colonia = @Colonia, Ciudad = @Ciudad, TipoEmpleado = @TipoEmpleado, NombreUsuario = @NombreUsuario" +
                    "Contraseña = @Contrasena, FechaUltimoAcceso = @FechaUltimoAcceso WHERE idEmpleado = @idEmpleado", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idEmpleado", empleado.idPersona));
                    command.Parameters.Add(new SqlParameter("@Nombre", empleado.Nombre));
                    command.Parameters.Add(new SqlParameter("@Apellido", empleado.Apellido));
                    command.Parameters.Add(new SqlParameter("@Telefono", empleado.Telefono));
                    command.Parameters.Add(new SqlParameter("@Email", empleado.Email));
                    command.Parameters.Add(new SqlParameter("@Calle", empleado.Calle));
                    command.Parameters.Add(new SqlParameter("@Numero", empleado.Numero));
                    command.Parameters.Add(new SqlParameter("@CodigoPostal", empleado.CodigoPostal));
                    command.Parameters.Add(new SqlParameter("@Colonia", empleado.Colonia));
                    command.Parameters.Add(new SqlParameter("@Ciudad", empleado.Ciudad));
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", empleado.NombreUsuario));
                    command.Parameters.Add(new SqlParameter("@Contrasena", empleado.Contraseña));
                    command.Parameters.Add(new SqlParameter("@FechaUltimoAcceso", empleado.FechaUltimoAcceso));
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", empleado.TipoEmpleado));

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                    }
                    catch (SqlException)
                    {
                        resultado = ResultadoOperacion.FalloSQL;
                        return resultado;
                    }
                }
            }
            return resultado;
        }

        public Empleado GetEmpleadoByUsername(String username)
        {
            Empleado empleado = new Empleado();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Empleado WHERE NombreUsuario = @usernameToSearch", connection))
                {
                    command.Parameters.Add(new SqlParameter("usernameToSearch", username));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                    }
                }
                connection.Close();
            }
            return empleado;
        }

        public List<Empleado> GetEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Empleado", connection))
                {
                    command.Parameters.Add(new SqlParameter());
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Empleado empleado = new Empleado();
                        empleado.idPersona = reader["idPersona"].ToString();
                        empleado.Nombre = reader["Nombre"].ToString();
                        empleado.Apellido = reader["Apellido"].ToString();
                        empleado.Telefono = reader["Telefono"].ToString();
                        empleado.Email = reader["Email"].ToString();
                        empleado.Calle = reader["Calle"].ToString();
                        empleado.Numero = reader["Numero"].ToString();
                        empleado.CodigoPostal = reader["CodigoPostal"].ToString();
                        empleado.Colonia = reader["Colonia"].ToString();
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                        empleado.Contraseña = reader["Contrasena"].ToString();
                        empleado.FechaUltimoAcceso = DateTime.Parse(reader["FechaUltimoAcceso"].ToString());
                        empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
                        listaEmpleados.Add(empleado);
                    }
                }
                connection.Close();
            }
            return listaEmpleados;
        }

        public List<Empleado> GetEmpleadosByDireccion(string Direccion)
        {
            throw new NotImplementedException();
        }

        public List<Empleado> GetEmpleadosByNombre(string Nombre)
        {
            throw new NotImplementedException();
        }

        public List<Empleado> GetEmpleadosByTelefono(string Telefono)
        {
            throw new NotImplementedException();
        }
    }
  }
