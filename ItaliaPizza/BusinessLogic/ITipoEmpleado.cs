using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface ITipoEmpleado
    {
        ResultadoOperacion AgregarTipoEmpleado(TipoEmpleado tipoEmpleado);
    }
}
