//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductoInventario
    {
        public int Inventario { get; set; }
        public int Producto { get; set; }
        public Nullable<int> CantidadIngreso { get; set; }
        public Nullable<double> PrecioCompra { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        public Nullable<System.DateTime> Caducidad { get; set; }
    
        public virtual Inventario Inventario1 { get; set; }
        public virtual Producto Producto1 { get; set; }
    }
}
