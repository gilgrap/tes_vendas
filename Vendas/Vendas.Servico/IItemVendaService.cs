using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Servico
{
    public interface IItemVendaService
    {
        void AddItemToVenda(Venda venda, ItemVenda item);
        void RemoveItemFromVenda(Venda venda, int itemId);
    }

}
