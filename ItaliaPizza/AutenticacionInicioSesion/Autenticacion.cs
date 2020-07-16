using DatabaseConnection;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AutenticacionInicioSesion
{
    public class Autenticacion
    {
        public enum validationResult
        {
            Success,
            UnknownFail,
            UserOrPasswordIncorrect,
            UserIncorrect,
            PasswordIncorrect
        }

        private String PassHash(String data)
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

        public validationResult AutenticacionCredenciales(String user, String pass)
        {
            validationResult result = validationResult.UnknownFail;
            DbConnection dbConnection = new DbConnection();
            String tipoString = "";
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT [TipoEmpleado] FROM [dbo].[Empleado] WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contraseña", connection))
                {
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", user));
                    command.Parameters.Add(new SqlParameter("@Contraseña", PassHash(pass)));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tipoString = reader["TipoEmpleado"].ToString();
                    }

                    if (String.IsNullOrEmpty(tipoString))
                    {
                        result = validationResult.UserOrPasswordIncorrect;
                    }
                    else
                    {
                        result = validationResult.Success;
                    }

                }

            }
            return result;
        }

        public String GetUserType(String user, String pass)
        {
            String result = "";
            DbConnection dbConnection = new DbConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT [TipoEmpleado] FROM [dbo].[Empleado] WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contraseña", connection))
                {
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", user));
                    command.Parameters.Add(new SqlParameter("@Contraseña", PassHash(pass)));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader["TipoEmpleado"].ToString();
                    }

                }

            }
            return result;
        }

        public String GetUserName(String user, String pass)
        {
            String result = "";
            DbConnection dbConnection = new DbConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT [NombreUsuario] FROM [dbo].[Empleado] WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contraseña", connection))
                {
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", user));
                    command.Parameters.Add(new SqlParameter("@Contraseña", PassHash(pass)));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader["NombreUsuario"].ToString();
                    }

                }
            }
            return result;
        }

        public String GetIdEmpleado(String user, String pass)
        {
            String result = "";
            DbConnection dbConnection = new DbConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT [idEmpleado] FROM [dbo].[Empleado] WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contraseña", connection))
                {
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", user));
                    command.Parameters.Add(new SqlParameter("@Contraseña", PassHash(pass)));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader["idEmpleado"].ToString();
                    }

                }
            }
            return result;
        }

        public String GetNombreEmpleado(String idEmpleado)
        {
            String result = "";
            DbConnection dbConnection = new DbConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT [Nombre] FROM [dbo].[Persona] WHERE idPersona = @idEmpleado", connection))
                {
                    command.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader["Nombre"].ToString();
                    }
                }
            }
            return result;
        }
    }
}
