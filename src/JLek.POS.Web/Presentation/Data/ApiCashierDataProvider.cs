using JLek.POS.Web.Presentation.Dtos;
using JLek.POS.Web.Presentation.Abstractions;
using JLek.POS.Web.Clients.Tables;
using JLek.POS.Web.Clients.Menu;

namespace JLek.POS.Web.Presentation.Data;

public sealed class ApiCashierDataProvider : ICashierDataProvider
{
    private readonly ITableClient _tableClient;
    private readonly IMenuClient _menuClient;

    public ApiCashierDataProvider(ITableClient tableClient, IMenuClient menuClient)
    {
        _tableClient = tableClient;
        _menuClient = menuClient;
    }

    public async Task<CashierDataDto> LoadAsync(CancellationToken cancellationToken = default)
    {
        var tablesTask = _tableClient.GetAllAsync(cancellationToken);
        var productsTask = _menuClient.GetProductsAsync(cancellationToken);

        await Task.WhenAll(tablesTask, productsTask);

        var tables = tablesTask.Result;
        var products = productsTask.Result;

        return new CashierDataDto
        {
            Tables = tables.Select(t => new TableDto
            {
                Id = t.Id,
                Name = t.Name,
                Status = t.Status,
            }).ToList(),

            MenuItems = products.Select(p => new MenuItemDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = string.Empty,
            }).ToList(),
        };
    }
}