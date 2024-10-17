using Moq;
using Pedidos.Domain.Interfaces;
using Pedidos.Domain.Model;
using Pedidos.Service.Services;


namespace Pedidos.Test.Service
{
    public class ProcessaPedidoServiceTests
    {
        private readonly ProcessaPedidoService _processaPedidoService;
        private readonly Mock<IEmbalagensServices> _embalagensServicesMock;

        public ProcessaPedidoServiceTests()
        {
            _embalagensServicesMock = new Mock<IEmbalagensServices>();
            _processaPedidoService = new ProcessaPedidoService(_embalagensServicesMock.Object);
        }

        [Fact]
        public void ProcessaEmbalagem_DeveRetornarPedidosCorretos()
        {
            // Arrange
            var respostaPedidos = new List<RespostaPedido>
            {
                new RespostaPedido
                {
                    PedidoId = 1,
                    Embalagens = new List<Embalagem>
                    {
                        new Embalagem { CaixaId = 1, Produto = new Produto { produto_id = "101" } },
                        new Embalagem { CaixaId = 1, Produto = new Produto { produto_id = "102" } }
                    }
                }
            };

            // Act
            var result = _processaPedidoService.ProcessaEmbalagem(respostaPedidos);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].pedido_id);
            Assert.Equal(1, result[0].caixaProdutos.caixa_Id);
            Assert.Equal("101 - 102", result[0].caixaProdutos.produto_Id);
        }

        [Fact]
        public void ProcessarPedido_DeveRetornarPedidosProcessados()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    pedido_id = 1,
                    produtos = new List<Produto>
                    {
                        new Produto { produto_id = "101", dimensoes = new Dimensoes { Altura = 10, Largura = 5, Comprimento = 2 } },
                        new Produto { produto_id = "102", dimensoes = new Dimensoes { Altura = 20, Largura = 10, Comprimento = 5 } }
                    }
                }
            };

            var embalagens = new List<Embalagem>
            {
                new Embalagem { CaixaId = 1, Produto = new Produto { produto_id = "101" } },
                new Embalagem { CaixaId = 1, Produto = new Produto { produto_id = "102" } }
            };

            _embalagensServicesMock.Setup(es => es.SelecionarCaixas(It.IsAny<List<Produto>>())).Returns(embalagens);

            // Act
            var result = _processaPedidoService.ProcessarPedido(pedidos);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].pedido_id);
            Assert.Equal(1, result[0].caixaProdutos.caixa_Id);
            Assert.Equal("101 - 102", result[0].caixaProdutos.produto_Id);
        }
    }
}
