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
    
    public partial class Inventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventario()
        {
            this.ProductoInventario = new HashSet<ProductoInventario>();
        }
    
        public int idInventario { get; set; }
        public Nullable<int> ExistenciaTotal { get; set; }
        public string UnidadMedida { get; set; }
        public Nullable<int> Producto { get; set; }
        public Nullable<int> ExistenciaInicial { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductoInventario> ProductoInventario { get; set; }
        public virtual Producto Producto1 { get; set; }
    }
}
