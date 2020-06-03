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
    }
}