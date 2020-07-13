using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class BaseDeDatosController
    {
        public ResultadoOperacionEnum.ResultadoOperacion IniciarRespaldo(String ruta)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            DatabaseDAO databaseDAO = new DatabaseDAO();
            resultado = databaseDAO.ObtenerRespaldo(ruta);
            return resultado;
        }
    }
}
