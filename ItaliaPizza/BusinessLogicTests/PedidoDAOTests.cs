using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.Fail();
        }

        [TestMethod()]
        public void PonerEnPreparacionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EsADomicilioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CambiarPedidoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CambiarProductosDePedidoTest()
        {
            Assert.Fail();
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
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPedidoPorIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerEstatusPorNombreTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerEstatusPorIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerListaPedidosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerListaPedidoProductoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerListaPedidosDisponiblesCocinaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerListaPedidosDisponiblesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerListaPedidosCallCenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPaginasDeTablaPedidoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPedidosPorFechaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPedidosPorRangoCocineroTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPedidosPorRangoCocineroTest1()
        {
            Assert.Fail();
        }
    }
}