﻿using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace Controller
{
    public class ProductoIngredienteController
    {
        public ResultadoOperacion crearProductoIngrediente(string nombre, string codigo, string descripcion, float precioUnitario,
            string restriccion, string unidadMedida, string ubicacion, int cantidad, string caducidad, string tipoIngrediente)
        {
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            ProductoIngrediente productoIngrediente = new ProductoIngrediente();
            Random random = new Random();

            productoIngrediente.Nombre = nombre;
            productoIngrediente.Código = codigo;
            productoIngrediente.Descripción = descripcion;
            productoIngrediente.Restricción = restriccion;
            productoIngrediente.tipoIngrediente = (TipoIngredienteEnum)Enum.Parse(typeof(TipoIngredienteEnum), tipoIngrediente);

            Inventario inventario = new Inventario();
            // inventario.Caducidad = DateTime.Parse(caducidad);
            inventario.Caducidad = caducidad;
            inventario.CantidadIngreso = cantidad;
            inventario.PrecioCompra = precioUnitario;
            inventario.Producto = productoIngrediente;
            inventario.ExistenciaTotal = cantidad;
            inventario.UnidadDeMedida = unidadMedida;
            inventario.FechaIngreso = DateTime.Now;


            ProductoIngredienteDAO productoIngredienteDAO = new ProductoIngredienteDAO();

           resultadoOperacion = productoIngredienteDAO.AddProductoIngrediente(productoIngrediente, inventario);

            return resultadoOperacion;


        }


        public List<ProductoIngrediente> ObtenerProductosIngrediente(int rango)
        {
            const int NUM_RESULTADOS = 19;
            rango -= 1;
            rango *= NUM_RESULTADOS;
            ProductoIngredienteDAO productoIngredienteDAO = new ProductoIngredienteDAO();
            List<ProductoIngrediente> productoIngredientes = productoIngredienteDAO.GetProductosIngrediente(rango);
            return productoIngredientes;
        }


        public ResultadoOperacion EliminarProductoIngrediente(Producto producto)
        {
            ProductoIngredienteDAO productoIngredienteDAO = new ProductoIngredienteDAO();
            ResultadoOperacion resultado = productoIngredienteDAO.EliminarProducto(producto.idProducto);
            return resultado;
        }

        public ProductoIngrediente buscarProductoIngredientePorId(int id)
        {
            ProductoIngredienteDAO productoIngredienteDAO = new ProductoIngredienteDAO();
            ProductoIngrediente productoIngrediente = productoIngredienteDAO.ObtenerProductoIngredientePorId(id);
            return productoIngrediente;
        }
    }
}
