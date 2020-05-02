﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProductoVenta: Producto
    {
        public float PreciPúblico { get; set; }
        public TipoProductoEnum TipoProducto { get; set; }
        public string FotoProducto { get; set; }
        public bool TieneReceta { get; set; }
        public Receta Receta { get; set; }

        public ProductoVenta() { }
        


    }

    public enum TipoProductoEnum
    {
    }
}