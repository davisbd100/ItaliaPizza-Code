using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface ICliente
    {
        ResultadoOperacion AgregarCliente(Cliente cliente);
        ResultadoOperacion EditarCliente(Cliente cliente);
        Cliente GetClineteByIdCliente(String idCliente);
        Cliente GetClienteId(String idCliente);
        List<Cliente> GetCliente(int rango);
        List<Cliente> BuscarCliente(string busqueda);
        List<Cliente> BuscarClienteDireccion(string busqueda);
        List<Cliente> BuscarClienteTelefono(string busqueda);
        ResultadoOperacion EliminarCliente(string idCliente);
    }
}
