using Pedidos.Domain.Interfaces;
using Pedidos.Domain.Model;
using Pedidos.Domain.ReturnPedidos;

namespace Pedidos.Service.Services
{
    public class ProcessaPedidoService : IProcessaPedidoService
    {
        private readonly IEmbalagensServices _embalagensServices;

        public ProcessaPedidoService(IEmbalagensServices embalagensServices)
        {
            _embalagensServices = embalagensServices;
        }

        public List<PedidosReturn> ProcessaEmbalagem(List<RespostaPedido> respostaPedidos)
        {
            string produtoNome = "";
            List<CaixaProdutos> caixaProdutos = new List<CaixaProdutos>();

            foreach (var item in respostaPedidos)
            {
                foreach(var itemName in item.Embalagens)
                {
                    produtoNome = itemName.Produto.produto_id.ToString();
                    CaixaProdutos caixaProduto = new CaixaProdutos();
                    caixaProduto.caixa_Id = itemName.CaixaId;
                    caixaProduto.produto_Id = itemName.Produto.produto_id;
                    caixaProduto.pedido_id = item.PedidoId;
                    caixaProdutos.Add(caixaProduto);
                }
            }
            List<PedidosReturn> pedidosReturns = new List<PedidosReturn>();
            string produtos = "";
            foreach (var item in caixaProdutos)
            {
                if (pedidosReturns.Where(x => x.pedido_id == item.pedido_id && x.caixaProdutos.caixa_Id == item.caixa_Id).Count() > 0)
                {
                    produtos = "";
                    continue;
                }

                PedidosReturn pedidosReturn = new PedidosReturn();

                CaixasProdutosReturn caixasProdutosReturn = new CaixasProdutosReturn();
                foreach(var produto in caixaProdutos)
                {
                    if(item.pedido_id == produto.pedido_id && item.caixa_Id == produto.caixa_Id)
                    {
                        produtos = produtos + " - " + produto.produto_Id;
                    }
                }
                caixasProdutosReturn.produto_Id = produtos.Substring(2, produtos.Length - 2);
                caixasProdutosReturn.caixa_Id = item.caixa_Id;
                pedidosReturn.pedido_id = item.pedido_id;
                pedidosReturn.caixaProdutos = caixasProdutosReturn;

                if (pedidosReturns.Where(x => x.pedido_id == item.pedido_id && x.caixaProdutos.caixa_Id == item.caixa_Id).Count() == 0)
                {
                    pedidosReturns.Add(pedidosReturn);
                }
            }
            return pedidosReturns;
        }

        public List<PedidosReturn> ProcessarPedido(List<Pedido> pedidos)
        {
            var resposta = new List<RespostaPedido>();

            foreach (var pedido in pedidos)
            {
                var embalagens = _embalagensServices.SelecionarCaixas(pedido.produtos);
                resposta.Add(new RespostaPedido { PedidoId = pedido.pedido_id, Embalagens = embalagens });
            }


            return ProcessaEmbalagem(resposta);
        }
    }
}
