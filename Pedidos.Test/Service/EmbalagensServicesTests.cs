using System.Collections.Generic;
using System.Linq;
using Pedidos.Domain.Interfaces;
using Pedidos.Domain.Model;
using Pedidos.Service.Services;
using Xunit;


namespace Pedidos.Test.Service
{
    public class EmbalagensServicesTests
    {
        private readonly EmbalagensServices _embalagensServices;

        public EmbalagensServicesTests()
        {
            _embalagensServices = new EmbalagensServices();
        }

        [Fact]
        public void CalcularVolume_DeveRetornarVolumeCorreto()
        {
            // Arrange
            var produto = new Produto
            {
                dimensoes = new Dimensoes { Altura = 10, Largura = 5, Comprimento = 2 }
            };

            // Act
            var volume = _embalagensServices.CalcularVolume(produto);

            // Assert
            Assert.Equal(100, volume);
        }

        [Fact]
        public void SelecionarCaixas_DeveRetornarCaixaCorretaParaProduto()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { produto_id = "1", dimensoes = new Dimensoes { Altura = 10, Largura = 5, Comprimento = 2 } },
                new Produto { produto_id = "2", dimensoes = new Dimensoes { Altura = 50, Largura = 40, Comprimento = 30 } }
            };

            // Act
            var embalagens = _embalagensServices.SelecionarCaixas(produtos);

            // Assert
            Assert.Equal(2, embalagens.Count);
            Assert.Equal(1, embalagens[0].CaixaId);
            Assert.Equal(3, embalagens[1].CaixaId);
        }

        [Fact]
        public void SelecionarCaixas_DeveRetornarListaVaziaSeNenhumaCaixaServir()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { produto_id = "1", dimensoes = new Dimensoes { Altura = 100, Largura = 100, Comprimento = 100 } }
            };

            // Act
            var embalagens = _embalagensServices.SelecionarCaixas(produtos);

            // Assert
            Assert.Empty(embalagens);
        }
    }
}
