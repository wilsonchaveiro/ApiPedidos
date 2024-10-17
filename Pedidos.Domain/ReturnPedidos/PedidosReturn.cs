using Pedidos.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Domain.ReturnPedidos
{
    public class PedidosReturn
    {
        public int pedido_id {  get; set; }
        public CaixasProdutosReturn caixaProdutos { get; set; }
    }
}
