using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItaliaPizza
{
    /// <summary> Clase que tiene como funcionalidad validar la estructura de los datos introducidos por el usuario</summary>
    public class ValidarCampos
    {
        public enum ResultadosValidación
        {
            NombreVálido,
            NombreInválido,
            ApellidoVálido,
            ApellidoInválido,
            TelefonoVálido,
            TelefónoInválido,
            CorreoVálido,
            CorreoInválido,
            CiudadValida,
            CiudadInválida,
            DirecciónVálida,
            DirecciónInválida,
            CódigoPostalVálido,
            CódigoPostalInválido,
            UsuarioVálido,
            UsuarioInválido,
            ContraseñaInválida,
            ContraseñaVálida
        }

        public ResultadosValidación ValidiarNombre(string nombre)
        {
            string patrón = @"^[a-z-A-Z\D]+$";
            if (Regex.IsMatch(nombre, patrón))
            {
                return ResultadosValidación.NombreVálido;
            }
            return ResultadosValidación.NombreInválido;
        }

        public ResultadosValidación ValidiarApellido(string apellido)
        {
            string patrón = @"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$";
            if (Regex.IsMatch(apellido, patrón))
            {
                return ResultadosValidación.ApellidoVálido;
            }
            return ResultadosValidación.ApellidoInválido;
        }

        public ResultadosValidación ValidarTelefono(string telefono)
        {
            string patrón = @"^[0-9]*$";
            if (Regex.IsMatch(telefono, patrón))
            {
                return ResultadosValidación.TelefonoVálido;
            }
            return ResultadosValidación.TelefónoInválido;
        }

        public ResultadosValidación ValidarCorreo(string correo)
        {
            string patrón = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (Regex.IsMatch(correo, patrón))
            {
                return ResultadosValidación.CorreoVálido;
            }
            return ResultadosValidación.CorreoInválido;
        }

        public ResultadosValidación ValidarCiudad(string ciudad)
        {
            string patrón = @"^[A-Za-z]+$";
            if (Regex.IsMatch(ciudad, patrón))
            {
                return ResultadosValidación.CiudadValida;
            }
            return ResultadosValidación.CiudadInválida;
        }

        //Validar ciudad
        //Validar dirección

        public ResultadosValidación ValidarCodigoPostal(string codigoPostal)
        {
            string patrón = @"^\d{5}-\d{4}|\d{5}|[A-Z]\d[A-Z] \d[A-Z]\d$";
            if (Regex.IsMatch(codigoPostal, patrón))
            {
                return ResultadosValidación.CódigoPostalVálido;
            }
            return ResultadosValidación.CódigoPostalInválido;
        }


        /// <summary>  Valida la estructura correcta de una contraseña. Debe contener por lo menos 8 caracteres, una mayúscula y un número</summary>
        /// <param name="contraseña">  contraseña.</param>
        /// <returns>Resultado de la validación</returns>
        public ResultadosValidación ValidarContraseña(string contraseña)
        {
            string patrón = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$";

            if (Regex.IsMatch(contraseña, patrón))
            {
                return ResultadosValidación.ContraseñaVálida;
            }
            return ResultadosValidación.ContraseñaInválida;

        }

    }
}
