using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProductoIngrediente: Producto
    {
        public TipoIngredienteEnum tipoIngrediente { get; set; }
    }

    public enum TipoIngredienteEnum
    {
        Aceite,
        Bebida,
        Carne,
        Cereal,
        Especia,
        Fruta,
        Harina,
        Hongo,
        Legumbre,
        Lácteo,
        Marisco,
        Salsa,
        Verdura,
        Otro
    }
}
