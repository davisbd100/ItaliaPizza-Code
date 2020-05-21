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
                    command.Parameters.Add(new SqlParameter("Calle", empleado.Calle));
                    command.Parameters.Add(new SqlParameter("Numero", empleado.Numero));
                    command.Parameters.Add(new SqlParameter("CodigoPostal", empleado.CodigoPostal));
                    command.Parameters.Add(new SqlParameter("Colonia", empleado.Colonia));
                    command.Parameters.Add(new SqlParameter("Ciudad", empleado.Ciudad));
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO dbo.Empleado VALUES (@idEmpleado, @TipoEmpleado, @NombreUsuario, @Contrasena, @FechaUltimoAcceso)";
                    command.Parameters.Add(new SqlParameter("@idEmpleado", empleado.idPersona));
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", empleado.TipoEmpleado));
                    command.Parameters.Add(new SqlParameter("NombreUsuario", empleado.NombreUsuario));
                    command.Parameters.Add(new SqlParameter("Contrasena", empleado.Contraseña));
                    command.Parameters.Add(new SqlParameter("FechaUltimoAcceso", empleado.FechaUltimoAcceso));

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
                    command.Parameters.Add(new SqlParameter("@TipoEmpleado", empleado.TipoEmpleado.ToString()));
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", empleado.NombreUsuario));
                    command.Parameters.Add(new SqlParameter("@Contrasena", empleado.Contraseña));
                    command.Parameters.Add(new SqlParameter("@FechaUltimoAcceso", empleado.FechaUltimoAcceso));

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

        public List<Empleado> GetEmpleados()
        {
            throw new NotImplementedException();
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
