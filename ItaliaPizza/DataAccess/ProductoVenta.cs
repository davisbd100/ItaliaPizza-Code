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
    
    public partial class ProductoVenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductoVenta()
        {
            this.PedidoProducto = new HashSet<PedidoProducto>();
        }
    
        public int idProductoVenta { get; set; }
        public Nullable<double> PrecioPublico { get; set; }
        public Nullable<int> TipoProducto { get; set; }
        public string FotoProducto { get; set; }
        public Nullable<bool> TieneReceta { get; set; }
        public Nullable<int> Receta { get; set; }
        public Nullable<bool> Visibilidad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoProducto> PedidoProducto { get; set; }
    }
}
