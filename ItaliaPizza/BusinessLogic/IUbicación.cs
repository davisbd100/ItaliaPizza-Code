using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;


namespace BusinessLogic
{
    interface IUbicación
    {
        List<Ubicación> ObtenerUbicacion();
        ResultadoOperacion AddUbicación(Ubicación ubicación);
        ResultadoOperacion EditarUbicación(Ubicación ubicación);
        ResultadoOperacion EliminarUbicación(Ubicación ubicación);
    }
}
