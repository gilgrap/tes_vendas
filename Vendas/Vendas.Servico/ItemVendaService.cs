using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Servico
{
    public class ItemVendaService : IItemVendaService
    {
        public void AddItemToVenda(Venda venda, ItemVenda item)
        {
            item.ValorTotal = item.ValorUnitario * item.Quantidade;
            //venda.Itens.Add(item);            
            CalcularValorTotal(venda);
        }

        public void RemoveItemFromVenda(Venda venda, int itemId)
        {
            var item = venda.Itens.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                venda.Itens.Remove(item);
                CalcularValorTotal(venda);
            }
        }

        private void CalcularValorTotal(Venda venda)
        {
            venda.ValorTotal = venda.Itens.Sum(item => item.ValorTotal);
        }
    }
}
