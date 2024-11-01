using Microsoft.AspNetCore.Mvc;
using Vendas.Domain;

[ApiController]
[Route("[controller]")]
public class VendasController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendasController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    // GET: /vendas
    [HttpGet]
    public IActionResult GetAll()
    {
        var vendas = _vendaService.GetAll();
        return Ok(vendas);
    }

    // GET: /vendas/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var venda = _vendaService.GetById(id);
        if (venda == null) return NotFound();
        return Ok(venda);
    }

    // POST: /vendas
    [HttpPost]
    public IActionResult Create([FromBody] Venda venda)
    {
        _vendaService.Create(venda);
        return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
    }

    // PUT: /vendas/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Venda venda)
    {
        if (id != venda.Id)
        {
            return BadRequest("O ID da venda não coincide.");
        }

        try
        {
            _vendaService.Update(venda);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: /vendas/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _vendaService.Delete(id);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }
}
