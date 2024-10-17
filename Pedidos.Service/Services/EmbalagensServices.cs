using Pedidos.Domain.Interfaces;
using Pedidos.Domain.Model;

namespace Pedidos.Service.Services
{
    public class EmbalagensServices : IEmbalagensServices
    {
        public int CalcularVolume(Produto produto)
        {
            return produto.dimensoes.Altura * produto.dimensoes.Largura * produto.dimensoes.Comprimento;
        }

        private static readonly List<Caixa> Caixas = new List<Caixa>
        {
            new Caixa { CaixaId= 1, Altura = 30, Largura = 40, Comprimento = 80 },
            new Caixa { CaixaId = 2, Altura = 80, Largura = 50, Comprimento = 40 },
            new Caixa { CaixaId = 3, Altura = 50, Largura = 80, Comprimento = 60 }
        };

        public List<Embalagem> SelecionarCaixas(List<Produto> produtos)
        {
            produtos = produtos.OrderByDescending(p => CalcularVolume(p)).ToList();
            var resultado = new List<Embalagem>();

            foreach (var produto in produtos)
            {
                foreach (var caixa in Caixas)
                {
                    if (produto.dimensoes.Altura <= caixa.Altura &&
                        produto.dimensoes.Largura <= caixa.Largura &&
                        produto.dimensoes.Comprimento <= caixa.Comprimento)
                    {
                        resultado.Add(new Embalagem { CaixaId = caixa.CaixaId, Produto = produto });
                        break;
                    }
                }
            }

            return resultado;
        }
    }
}
