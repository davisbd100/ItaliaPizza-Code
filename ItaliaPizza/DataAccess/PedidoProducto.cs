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
    
    public partial class PedidoProducto
    {
        public int idPedido { get; set; }
        public int idProductoVenta { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<double> Precio { get; set; }
    
        public virtual Pedido Pedido { get; set; }
        public virtual ProductoVenta ProductoVenta { get; set; }
    }
}
