using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace Controller
{
    public class InventarioController
    {

        public List<DataAccess.Inventario> ObtenerInventario()
        {
            List<DataAccess.Inventario> resultado = new List<DataAccess.Inventario>();
            InventarioDAO inventarioDAO = new InventarioDAO();
            resultado = inventarioDAO.ObtenerTodosLosInventarios();
            return resultado;
        }

    }
}
