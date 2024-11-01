using Microsoft.EntityFrameworkCore;
using Vendas.Domain;
using Vendas.Servico;

public class VendaServiceTests : IDisposable
{
    private VendasDbContext _context;
    private VendaService _vendaService;

    public VendaServiceTests()
    {
        var options = new DbContextOptionsBuilder<VendasDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome único para o banco
            .Options;

        _context = new VendasDbContext(options);
        var itemVendaService = new ItemVendaService();
        var eventPublisher = new VendaEventPublisher();
        _vendaService = new VendaService(_context, itemVendaService, eventPublisher);
    }

    [Fact]
    public void Create_AddsVenda()
    {
        // Arrange
        var venda = new Venda
        {
            Itens = new List<ItemVenda>
            {
                new ItemVenda { ProdutoId = 1, Quantidade = 2, ValorUnitario = 10.0m }
            }
        };

        // Act
        _vendaService.Create(venda);

        // Assert
        Assert.Single(_context.Vendas);
        Assert.Equal(1, _context.Vendas.First().Id); // O ID deve ser gerado automaticamente
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted(); // Remove o banco em memória após os testes
        _context.Dispose();
    }
}
