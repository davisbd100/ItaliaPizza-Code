using System;
using System.Collections.Generic;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface IEmpleado
    {
        ResultadoOperacion AgregarEmpleado(Empleado empleado);
        ResultadoOperacion EditarEmpleado(Empleado empleado);
        ResultadoOperacion EditarEmpleadoUsuario(Empleado empleado);
        Empleado GetEmpleadoByUsername(String username);
        List<Empleado> GetEmpleados(int rango);
        List<Empleado> BuscarEmpleado(string busqueda);
        List<Empleado> BuscarEmpleadoDireccion(string busqueda);
        List<Empleado> BuscarEmpleadoTelefono(string busqueda);
        ResultadoOperacion EliminarEmpleado(string idEmpleado);
    }
}
