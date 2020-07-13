using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using DatabaseConnection;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    public class EmpleadoDAO : IEmpleado
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

        public ResultadoOperacion AgregarEmpleado(Empleado empleado)
        {
            const int VALORES_DUPLICADOS = 2601;

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
                         "INSERT INTO dbo.Persona VALUES (@idPersona, @Nombre, @Apellido, @Telefono, @Email, @Calle, @Numero, @CodigoPostal, @Colonia, @Ciudad, 'TRUE')";
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
                        "INSERT INTO dbo.Empleado VALUES (@idEmpleado, @TipoEmpleado, @NombreUsuario, @Contrasena, @FechaUltimoAcceso, 'TRUE')";
                    command.Parameters.Add(new SqlParameter("@idEmpleado", empleado.idEmpleado));
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", empleado.TipoEmpleado));
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", empleado.NombreUsuario));
                    command.Parameters.Add(new SqlParameter("@Contrasena", PassHash(empleado.Contraseña)));
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
                            break;
                    }
                }
            }

            return resultado;
        }

        public ResultadoOperacion EditarEmpleado(Empleado empleado)
        {
            const int VALORES_DUPLICADOS = 2601;

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("EditarEmpleado");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "UPDATE dbo.Persona SET Nombre = @Nombre, Apellido = @Apellido, " +
                         "Telefono = @Telefono, Email = @Email, Calle = @Calle, Numero = @Numero, " +
                         "CodigoPostal = @CodigoPostal, Colonia = @Colonia, Ciudad = @Ciudad " +
                         "WHERE idPersona = @idPersona";
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

        public ResultadoOperacion EditarEmpleadoUsuario(Empleado empleado)
        {
            const int VALORES_DUPLICADOS = 2601;

            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbconnection = new DbConnection();

            using (SqlConnection connection = dbconnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("EditarEmpleadoUsuario");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    command.CommandText =
                         "UPDATE dbo.Empleado SET TipoEmpleado = @TipoEmpleado, NombreUsuario = @NombreUsuario, " +
                         "Contrasena = @Contrasena WHERE idEmpleado = @idEmpleado";
                    command.Parameters.Add(new SqlParameter("@idEmpleado", empleado.idEmpleado));
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", empleado.TipoEmpleado));
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", empleado.NombreUsuario));
                    command.Parameters.Add(new SqlParameter("@Contrasena", PassHash(empleado.Contraseña)));
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

        public Empleado GetEmpleadoid(String idEmpleado)
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Persona left join " +
                    "dbo.Empleado ON dbo.Persona.idPersona = dbo.Empleado.idEmpleado WHERE idPersona = @idEmpleado", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        empleado.idPersona = reader["idPersona"].ToString();
                        empleado.Nombre = reader["Nombre"].ToString();
                        empleado.Apellido = reader["Apellido"].ToString();
                        empleado.Telefono = reader["Telefono"].ToString();
                        empleado.Email = reader["Email"].ToString();
                        empleado.Calle = reader["Calle"].ToString();
                        empleado.Numero = reader["Numero"].ToString();
                        empleado.CodigoPostal = reader["CodigoPostal"].ToString();
                        empleado.Colonia = reader["Colonia"].ToString();
                        empleado.Ciudad = reader["Ciudad"].ToString();
                        empleado.idEmpleado = reader["idEmpleado"].ToString();
                        empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                        empleado.Contraseña = PassHash(reader["Contrasena"].ToString());
                        empleado.FechaUltimoAcceso = DateTime.Parse(reader["FechaUltimoAcceso"].ToString());
                    }
                }
                connection.Close();
            }
            return empleado;
        }

        public List<Empleado> GetEmpleados(int rango)
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
                using (SqlCommand command = new SqlCommand("SELECT idPersona, Nombre, Apellido, Telefono, Email, Calle, " +
                    "Numero, CodigoPostal, Colonia, Ciudad, TipoEmpleado, NombreUsuario FROM dbo.Persona left join " +
                    "dbo.Empleado ON dbo.Persona.idPersona = dbo.Empleado.idEmpleado  WHERE dbo.Persona.Visibilidad = 'TRUE' " +
                    "and idPersona = idEmpleado order by Nombre offset @Rango " +
                    "rows fetch next 20 rows only", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Rango", rango));
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
                        empleado.Ciudad = reader["Ciudad"].ToString();
                        empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                        listaEmpleados.Add(empleado);
                    }
                }
                connection.Close();
            }
            return listaEmpleados;
        }

        public List<Empleado> BuscarEmpleado(string busqueda)
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
                using (SqlCommand command = new SqlCommand("SELECT idPersona, Nombre, Apellido, Telefono, Email, Calle, " +
                    "Numero, CodigoPostal, Colonia, Ciudad, TipoEmpleado, NombreUsuario, FechaUltimoAcceso FROM dbo.Persona left join " +
                    "dbo.Empleado ON dbo.Persona.idPersona = dbo.Empleado.idEmpleado WHERE Nombre LIKE @Busqueda and idPersona = idEmpleado", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Busqueda", busqueda));
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
                        empleado.Ciudad = reader["Ciudad"].ToString();
                        empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                        empleado.FechaUltimoAcceso = DateTime.Parse(reader["FechaUltimoAcceso"].ToString());
                        listaEmpleados.Add(empleado);
                    }
                }
                connection.Close();
            }
            return listaEmpleados;
        }

        public List<Empleado> BuscarEmpleadoDireccion(string busqueda)
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
                using (SqlCommand command = new SqlCommand("SELECT idPersona, Nombre, Apellido, Telefono, Email, Calle, " +
                    "Numero, CodigoPostal, Colonia, Ciudad, TipoEmpleado, NombreUsuario, FechaUltimoAcceso FROM dbo.Persona left join " +
                    "dbo.Empleado ON dbo.Persona.idPersona = dbo.Empleado.idEmpleado WHERE Calle LIKE @Busqueda and idPersona = idEmpleado", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Busqueda", busqueda));
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
                        empleado.Ciudad = reader["Ciudad"].ToString();
                        empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                        empleado.FechaUltimoAcceso = DateTime.Parse(reader["FechaUltimoAcceso"].ToString());
                        listaEmpleados.Add(empleado);
                    }
                }
                connection.Close();
            }
            return listaEmpleados;
        }

        public List<Empleado> BuscarEmpleadoTelefono(string busqueda)
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
                using (SqlCommand command = new SqlCommand("SELECT idPersona, Nombre, Apellido, Telefono, Email, Calle, " +
                    "Numero, CodigoPostal, Colonia, Ciudad, TipoEmpleado, NombreUsuario, FechaUltimoAcceso FROM dbo.Persona left join " +
                    "dbo.Empleado ON dbo.Persona.idPersona = dbo.Empleado.idEmpleado WHERE Telefono LIKE @Busqueda and idPersona = idEmpleado", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Busqueda", busqueda));
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
                        empleado.Ciudad = reader["Ciudad"].ToString();
                        empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
                        empleado.NombreUsuario = reader["NombreUsuario"].ToString();
                        empleado.FechaUltimoAcceso = DateTime.Parse(reader["FechaUltimoAcceso"].ToString());
                        listaEmpleados.Add(empleado);
                    }
                }
                connection.Close();
            }
            return listaEmpleados;
        }

        public ResultadoOperacion EliminarEmpleado(string idEmpleado)
        {
            const int VALORES_DUPLICADOS = 2601;
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;
            DbConnection dbConnection = new DbConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("EliminarEmpleado");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "UPDATE dbo.Persona SET VISIBILIDAD = 'FALSE' WHERE idPersona = @idPersona";
                    command.Parameters.Add(new SqlParameter("@idPersona", idEmpleado));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "UPDATE dbo.Empleado SET VISIBILIDAD = 'FALSE' WHERE idEmpleado = @idEmpleado";
                    command.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
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
    }
  }
