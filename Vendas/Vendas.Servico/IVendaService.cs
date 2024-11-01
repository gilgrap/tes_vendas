using Vendas.Domain;

public interface IVendaService
{
    IEnumerable<Venda> GetAll();
    Venda GetById(int id);
    void Create(Venda venda);
    void Update(Venda venda);
    void Delete(int id);
}