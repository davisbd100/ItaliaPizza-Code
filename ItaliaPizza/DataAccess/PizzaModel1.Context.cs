﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PizzaEntities : DbContext
    {
        public PizzaEntities()
            : base("name=PizzaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Estatus> Estatus { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoDomicilio> PedidoDomicilio { get; set; }
        public virtual DbSet<PedidoProducto> PedidoProducto { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoIngrediente> ProductoIngrediente { get; set; }
        public virtual DbSet<ProductoInventario> ProductoInventario { get; set; }
        public virtual DbSet<Receta> Receta { get; set; }
        public virtual DbSet<RecetaIngrediente> RecetaIngrediente { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
        public virtual DbSet<UbicacionProducto> UbicacionProducto { get; set; }
    }
}
