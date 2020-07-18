using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic.Tests
{
    [TestClass()]
    public class PedidoDAOTests
    {
        [TestMethod()]
        public void CambiarEstadoPedidoSinConexionTest()
        {
            Pedido pedido = new Pedido()
            {
                idPedido = 1
            };
            DataAccess.Estatus estatus = new DataAccess.Estatus()
            {
                idEstatus = 1
            };
            PedidoDAO pedidoDAO = new PedidoDAO();
            Assert.AreEqual(ResultadoOperacionEnum.ResultadoOperacion.FalloSQL, pedidoDAO.CambiarEstadoPedido(pedido, estatus));
        }

        [TestMethod()]
        public void CambiarEstadoPedidoExitoTest()
        {
            Pedido pedido = new Pedido()
            {
                idPedido = 1
            };
            DataAccess.Estatus estatus = new DataAccess.Estatus()
            {
                idEstatus = 1
            };
            PedidoDAO pedidoDAO = new PedidoDAO();
            Assert.AreEqual(ResultadoOperacionEnum.ResultadoOperacion.Exito, pedidoDAO.CambiarEstadoPedido(pedido, estatus));
        }

        [TestMethod()]
        public void CambiarEstadoPedidoTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            pedidoDAO.CambiarEstadoPedido(new Pedido() { idPedido = 283 }, pedidoDAO.ObtenerEstatusPorNombre("Cancelado"));
            Assert.AreEqual(pedidoDAO.ObtenerListaPedidos().Where(b => b.idPedido == 283).FirstOrDefault().Estatus1.NombreEstatus, "Cancelado");
        }

        [TestMethod()]
        public void AsignarEntregaTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            pedidoDAO.AsignarEntrega(283, "Chuchito");
            Assert.AreEqual(pedidoDAO.ObtenerListaPedidos().Where(b => b.idPedido == 283).FirstOrDefault().Estatus1.NombreEstatus, "En Camino");
        }

        [TestMethod()]
        public void PonerEnPreparacionTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            pedidoDAO.PonerEnPreparacion(283);
            Assert.AreEqual(pedidoDAO.ObtenerListaPedidos().Where(b => b.idPedido == 283).FirstOrDefault().Estatus1.NombreEstatus, "En Preparación");
        }

        [TestMethod()]
        public void EsADomicilioFalseTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            Assert.IsFalse(pedidoDAO.EsADomicilio(283));
        }
        [TestMethod()]
        public void EsADomicilioTrueTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            Assert.IsTrue(pedidoDAO.EsADomicilio(543));
        }

        [TestMethod()]
        public void CambiarProductosDePedidoTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            ProductoVentaDAO productoDAO = new ProductoVentaDAO();
            List<DataAccess.PedidoProducto> pedidos = new List<DataAccess.PedidoProducto>();
            pedidos.Add(new DataAccess.PedidoProducto()
            {
                Cantidad = 2,
                idProductoVenta = 1,
                Precio = 64,
                idPedido = 283,
                ProductoVenta = productoDAO.ObtenerProductoVentaPoridEE(1)
            });
            Assert.AreEqual(pedidoDAO.CambiarProductosDePedido(283, pedidos), ResultadoOperacionEnum.ResultadoOperacion.Exito);
        }

        [TestMethod()]
        public void CrearPedidoDomicilioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CrearPedidoMeseroTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPedidoConProductoPorIdTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            DataAccess.Pedido tempPedido = pedidoDAO.GetPedidoConProductoPorId(283);
            Assert.AreEqual(tempPedido.idPedido, 283);
        }

        [TestMethod()]
        public void GetPedidoPorIdTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            DataAccess.Pedido tempPedido = pedidoDAO.GetPedidoConProductoPorId(283);
            Assert.AreEqual(tempPedido.idPedido, 283);
        }

        [TestMethod()]
        public void ObtenerEstatusPorNombreTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            DataAccess.Estatus tempEstatus = pedidoDAO.ObtenerEstatusPorNombre("Cancelado");
            Assert.AreEqual(tempEstatus.NombreEstatus, "Cancelado");
        }

        [TestMethod()]
        public void ObtenerEstatusPorIdTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            DataAccess.Estatus tempEstatus = pedidoDAO.ObtenerEstatusPorId(1);
            Assert.AreEqual(tempEstatus.idEstatus, 1);
        }

        [TestMethod()]
        public void ObtenerListaPedidosTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerListaPedidos();
            Assert.IsTrue(listaPedidos.Any());
        }

        [TestMethod()]
        public void ObtenerListaPedidoProductoTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerListaPedidoProducto(283);
            Assert.IsTrue(listaPedidos.Any());
        }

        [TestMethod()]
        public void ObtenerListaPedidosDisponiblesCocinaTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerListaPedidosDisponiblesCocina();
            bool resultado = true;
            foreach (var item in listaPedidos)
            {
                if (item.Estatus1.NombreEstatus == "Cancelado" || item.Estatus1.NombreEstatus == "Finalizado" || item.Estatus1.NombreEstatus == "En Camino" || item.Estatus1.NombreEstatus == "Entregado" || item.Estatus1.NombreEstatus == "Preparado")
                {
                    resultado = false;
                }
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerListaPedidosDisponiblesTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerListaPedidosDisponibles();
            bool resultado = true;
            foreach (var item in listaPedidos)
            {
                if (item.Estatus1.NombreEstatus == "Cancelado" || item.Estatus1.NombreEstatus == "Finalizado")
                {
                    resultado = false;
                }
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerListaPedidosCallCenterTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerListaPedidosCallCenter();
            bool resultado = true;
            foreach (var item in listaPedidos)
            {
                if (item.Estatus1.NombreEstatus == "Cancelado" || item.Estatus1.NombreEstatus == "Finalizado")
                {
                    resultado = false;
                }
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerPaginasDeTablaPedidoTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerPaginasDeTablaPedido(20);
            bool resultado = false;
            if (listaPedidos > -1)
            {
                resultado = true;
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerPedidosPorFechaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPedidosPorRangoCocineroTest()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            var listaPedidos = pedidoDAO.ObtenerPedidosPorRangoCocinero(20, 1);
            bool resultado = true;
            foreach (var item in listaPedidos)
            {
                if (item.Estatus1.NombreEstatus == "Cancelado" || item.Estatus1.NombreEstatus == "Finalizado")
                {
                    resultado = false;
                }
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void FallaCrearPedidoDomicilioTest1()
        {
            List<PedidoProducto> pedidoProductos = new List<PedidoProducto>();
            DataAccess.Cliente cliente = new DataAccess.Cliente();
            DataAccess.Pedido pedido = new DataAccess.Pedido();
            pedido.Cliente1 = cliente;
            pedido.Estatus = 2;
            pedido.FechaPedido = DateTime.Now;

            PedidoDAO pedidoDAO = new PedidoDAO();
            Assert.AreEqual(pedidoDAO.CrearPedidoDomicilio(pedido, pedidoProductos), ResultadoOperacionEnum.ResultadoOperacion.FalloSQL);

        }

        [TestMethod()]
        public void CrearPedidoMeseroTest1()
        {
            List<PedidoProducto> pedidoProductos = new List<PedidoProducto>();
            DataAccess.Cliente cliente = new DataAccess.Cliente();
            DataAccess.Pedido pedido = new DataAccess.Pedido();
            pedido.Cliente1 = cliente;
            pedido.Estatus = 2;
            pedido.NumeroMesa = 2;
            pedido.FechaPedido = DateTime.Now;

            PedidoDAO pedidoDAO = new PedidoDAO();
            Assert.AreEqual(pedidoDAO.CrearPedidoMesero(pedido, pedidoProductos), ResultadoOperacionEnum.ResultadoOperacion.FalloSQL);

        }
    }
}