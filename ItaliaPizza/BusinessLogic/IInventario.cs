using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface IInventario
    {
        List<Inventario> ObtenerInventarios();
        ResultadoOperacion AddInventario(Inventario inventario);


    }
}
