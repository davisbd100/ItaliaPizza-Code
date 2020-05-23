using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    interface ITipoProducto
    {
        ResultadoOperacionEnum.ResultadoOperacion addTipoProducto(TipoProducto tipoProducto);
    }
}
