using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Vendas.Domain;
using Vendas.Servico;
using Xunit;

public class VendasControllerTests
{
    private readonly VendasController _controller;
    private readonly Mock<IVendaService> _mockVendaService;

    public VendasControllerTests()
    {
        _mockVendaService = new Mock<IVendaService>();
        _controller = new VendasController(_mockVendaService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkResult_WithVendas()
    {
        // Arrange
        var vendas = new List<Venda> { new Venda { Id = 1 }, new Venda { Id = 2 } };
        _mockVendaService.Setup(service => service.GetAll()).Returns(vendas);

        // Act
        var result = _controller.GetAll() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(vendas, result.Value);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenVendaDoesNotExist()
    {
        // Arrange
        _mockVendaService.Setup(service => service.GetById(1)).Returns((Venda)null);

        // Act
        var result = _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        // Arrange
        var venda = new Venda { Id = 1 };
        _mockVendaService.Setup(service => service.Create(venda));

        // Act
        var result = _controller.Create(venda) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(venda, result.Value);
    }

    [Fact]
    public void Update_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var venda = new Venda { Id = 1 };
        _mockVendaService.Setup(service => service.Update(venda));

        // Act
        var result = _controller.Update(1, venda);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        _mockVendaService.Setup(service => service.Delete(1));

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
