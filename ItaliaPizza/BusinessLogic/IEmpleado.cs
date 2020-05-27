using System;
using System.Collections.Generic;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface IEmpleado
    {
        List<Empleado> GetEmpleados(int rango);
        List<Empleado> GetEmpleadosByNombre(String Nombre);
        List<Empleado> GetEmpleadosByTelefono(String Telefono);
        List<Empleado> GetEmpleadosByDireccion(String Direccion);
        ResultadoOperacion AgregarEmpleado(Empleado empleado);
        ResultadoOperacion EditarEmpleado(Empleado empleado);
        ResultadoOperacion DarDeBajaEmpleado(Empleado empleado);
    }
}
