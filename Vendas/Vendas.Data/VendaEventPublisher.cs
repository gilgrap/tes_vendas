using Serilog;
using Vendas.Domain;

public class VendaEventPublisher
{
    public void PublishCompraCriada(Venda venda)
    {
        Log.Information($"Evento: CompraCriada - Venda ID: {venda.Id}, Valor Total: {venda.ValorTotal}");
    }

    public void PublishCompraAlterada(Venda venda)
    {
        Log.Information($"Evento: CompraAlterada - Venda ID: {venda.Id}");
    }

    public void PublishCompraCancelada(Venda venda)
    {
        Log.Information($"Evento: CompraCancelada - Venda ID: {venda.Id}");
    }

    public void PublishItemCancelado(ItemVenda item)
    {
        Log.Information($"Evento: ItemCancelado - Item ID: {item.Id}, Venda ID: {item.VendaId}");
    }
}
