using Microsoft.EntityFrameworkCore;
using Vendas.Domain;
using Vendas.Servico;

public class VendaService : IVendaService
{
    private readonly VendasDbContext _context;
    private readonly IItemVendaService _itemVendaService;
    private readonly VendaEventPublisher _eventPublisher;
    public VendaService(VendasDbContext context, IItemVendaService itemVendaService, VendaEventPublisher eventPublisher)
    {
        _context = context;
        _itemVendaService = itemVendaService;
        _eventPublisher = eventPublisher;
    }
    public IEnumerable<Venda> GetAll()
    {
        return _context.Vendas.Include(v => v.Itens).ToList();
    }

    public Venda GetById(int id)
    {
        return _context.Vendas.Include(v => v.Itens).FirstOrDefault(v => v.Id == id);
    }

    public void Create(Venda venda)
    {
        foreach (var item in venda.Itens)
        {
            _itemVendaService.AddItemToVenda(venda, item);
        }
        venda.Itens = venda.Itens ?? new List<ItemVenda>();
        _context.Vendas.Add(venda);
        _context.SaveChanges();
        _eventPublisher.PublishCompraCriada(venda);
    }

    public void Update(Venda venda)
    {
        var vendaExistente = _context.Vendas.Include(v => v.Itens).FirstOrDefault(v => v.Id == venda.Id);
        if (vendaExistente == null) throw new Exception("Venda não encontrada.");

        // Atualiza as propriedades da venda existente
        vendaExistente.DataVenda = venda.DataVenda;
        vendaExistente.ClienteId = venda.ClienteId;
        vendaExistente.FilialId = venda.FilialId;
        vendaExistente.Cancelado = venda.Cancelado;

        vendaExistente.Itens.Clear();

        // Adiciona os novos itens da venda
        foreach (var item in venda.Itens)
        {
            item.ValorTotal = item.ValorUnitario * item.Quantidade; // Calcula o valor total do item
            vendaExistente.Itens.Add(item);
        }

        CalcularValorTotal(vendaExistente);

        _context.Vendas.Update(vendaExistente);
        _context.SaveChanges();
        _eventPublisher.PublishCompraAlterada(venda);
    }

    public void Delete(int id)
    {
        var venda = _context.Vendas.Include(v => v.Itens).FirstOrDefault(v => v.Id == id);
        if (venda == null) throw new Exception("Venda não encontrada.");

        _context.Vendas.Remove(venda);
        _context.SaveChanges();
        _eventPublisher.PublishCompraCancelada(venda);
    }

    private void CalcularValorTotal(Venda venda)
    {
        venda.ValorTotal = venda.Itens.Sum(item => item.ValorTotal);
    }
}