using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class VentaDAO : IVenta
    {
        public ResultadoOperacionEnum.ResultadoOperacion GuardarDiaVenta(DiaVenta venta)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            using (var context = new DataAccess.PizzaEntities())
            {
                try
                {
                    context.DiaVenta.Add(venta);
                    context.SaveChanges();
                    resultado = ResultadoOperacionEnum.ResultadoOperacion.Exito;
                }
                catch (EntityException)
                {
                    resultado = ResultadoOperacionEnum.ResultadoOperacion.FalloSQL;
                }
            }
            return resultado;
        }
    }
}
