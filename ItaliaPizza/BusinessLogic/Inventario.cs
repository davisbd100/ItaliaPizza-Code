using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Inventario
    {
        public Producto Producto { get; set; }
        public int CantidadIngreso { get; set; }
        public float PrecioCompra { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime Caducidad { get; set; }
        public int ExistenciaTotal { get; set; }
        public UnidadDeMedidaEnum UnidadDeMedida { get; set; }

        public Inventario() { }

    }

    public enum UnidadDeMedidaEnum
    {
        Kilo,
        Gramo,
        Litro,
        Mililitro,
        Cucharada,
        Taza,
        Galón
    }

}
