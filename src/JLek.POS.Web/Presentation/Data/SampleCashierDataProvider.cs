using JLek.POS.Web.Presentation.Dtos;
using JLek.POS.Web.Presentation.Abstractions;
using JLek.POS.Web.Presentation.SampleData;

namespace JLek.POS.Web.Presentation.Data;

public sealed class SampleCashierDataProvider : ICashierDataProvider
{
    public Task<CashierDataDto> LoadAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new CashierDataDto
        {
            Tables = new List<TableDto>
            {
                new() { Id = CashierSampleData.Table1Id, Name = "Table 1", Status = "Available" },
                new() { Id = CashierSampleData.Table2Id, Name = "Table 2", Status = "Available" },
                new() { Id = CashierSampleData.Table3Id, Name = "Table 3", Status = "Available" },
                new() { Id = CashierSampleData.Table4Id, Name = "Table 4", Status = "Available" },
            },
            MenuItems = new List<MenuItemDto>
            {
                new() { Id = CashierSampleData.PadThaiId, Name = "Pad Thai", Price = 120, Category = "Main" },
                new() { Id = CashierSampleData.TomYumId, Name = "Tom Yum", Price = 180, Category = "Main" },
                new() { Id = CashierSampleData.FriedRiceId, Name = "Fried Rice", Price = 95, Category = "Main" },
                new() { Id = CashierSampleData.ThaiTeaId, Name = "Thai Tea", Price = 55, Category = "Drink" },
                new() { Id = CashierSampleData.MangoStickyRiceId, Name = "Mango Sticky Rice", Price = 140, Category = "Dessert" },
                new() { Id = CashierSampleData.WaterId, Name = "Water", Price = 20, Category = "Drink" },
            },
        });
    }
}