using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Receta
    {
        public int IdReceta { get; set; }
        public string Nombre { get; set; }
        public string Procedimiento { get; set; }
        public float Rendimiento { get; set; }
        public List<Tuple<ProductoIngrediente, float, float>>  Ingredientes { get; set; }

        public Receta() { 
            
        }
    }
    
}
