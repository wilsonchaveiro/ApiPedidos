using ApiPedidos.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pedidos.Domain.Interfaces;
using Pedidos.Domain.Model;
using Pedidos.Domain.ReturnPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Test.Application
{
    public class PedidosControllerTests
    {
        private readonly PedidosController _pedidosController;
        private readonly Mock<IProcessaPedidoService> _processaPedidoServiceMock;

        public PedidosControllerTests()
        {
            _processaPedidoServiceMock = new Mock<IProcessaPedidoService>();
            _pedidosController = new PedidosController(_processaPedidoServiceMock.Object);
        }

        [Fact]
        public void ProcessarPedidos_DeveRetornarOkResultComResposta()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido { pedido_id = 1, produtos = new List<Produto> { new Produto { produto_id = "101" } } }
            };

            var pedidosReturn = new List<PedidosReturn>
            {
                new PedidosReturn
                {
                    pedido_id = 1,
                    caixaProdutos = new CaixasProdutosReturn
                    {
                        caixa_Id = 1,
                        produto_Id = "101"
                    }
                }
            };

            _processaPedidoServiceMock.Setup(service => service.ProcessarPedido(pedidos)).Returns(pedidosReturn);

            // Act
            var result = _pedidosController.ProcessarPedidos(pedidos);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PedidosReturn>>(okResult.Value);
            Assert.Equal(pedidosReturn, returnValue);
        }

        [Fact]
        public void ProcessarPedidos_DeveRetornarBadRequestQuandoPedidosForemNulos()
        {
            // Act
            var result = _pedidosController.ProcessarPedidos(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
