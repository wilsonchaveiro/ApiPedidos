using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Domain.Model
{
    public class RespostaPedido
    {
        public int PedidoId { get; set; }
        public List<Embalagem> Embalagens { get; set; }
    }
}
