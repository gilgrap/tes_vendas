using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Vendas.Domain;

public class VendasDbContext : DbContext
{
    public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options) { }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemVenda> ItensVenda { get; set; }
}
