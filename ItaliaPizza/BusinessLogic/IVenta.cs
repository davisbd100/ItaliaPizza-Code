using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    interface IVenta
    {
        ResultadoOperacionEnum.ResultadoOperacion GuardarDiaVenta(DataAccess.DiaVenta venta);
    }
}
