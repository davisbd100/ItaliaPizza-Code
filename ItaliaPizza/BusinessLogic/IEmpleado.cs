using System;
using System.Collections.Generic;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface IEmpleado
    {
        ResultadoOperacion AgregarEmpleado(Empleado empleado);
        ResultadoOperacion EditarEmpleado(Empleado empleado);
        Empleado GetEmpleadoByUsername(String username);
        Empleado GetEmpleadoId(String idEmpleado);
        List<Empleado> GetEmpleados(int rango);
        List<Empleado> BuscarEmpleado(string busqueda);
        List<Empleado> BuscarEmpleadoDireccion(string busqueda);
        List<Empleado> BuscarEmpleadoTelefono(string busqueda);
        ResultadoOperacion EliminarEmpleado(string idEmpleado);
    }
}
