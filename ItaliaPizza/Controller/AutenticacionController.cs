using AutenticacionInicioSesion;
using System;
using static AutenticacionInicioSesion.Autenticacion;

namespace Controller
{
    public class DatosLogin
    {
        public String TipoEmpleado { get; set; }
        public validationResult Result { get; set; }
    } 

    public class AutenticacionController
    {
        public DatosLogin AutenticacionEmpleado(String user, String pass)
        {
            DatosLogin datosLogin = new DatosLogin();
            Autenticacion autenticacion = new Autenticacion();
            if (autenticacion.AutenticacionCredenciales(user, pass).Equals(validationResult.Success))
            {
                datosLogin.TipoEmpleado = autenticacion.GetUserType(user, pass);
                datosLogin.Result = validationResult.Success;
            }
            else
            {
                datosLogin.Result = validationResult.UserOrPasswordIncorrect;
            }
            return datosLogin;
        }

        public String GetUserName(String user, String pass)
        {
            Autenticacion autenticacion = new Autenticacion();
            return autenticacion.GetUserName(user, pass);

        }
        public String GetUserType(String user, String pass)
        {
            Autenticacion autenticacion = new Autenticacion();
            return autenticacion.GetUserType(user, pass);
        }

    }
}
