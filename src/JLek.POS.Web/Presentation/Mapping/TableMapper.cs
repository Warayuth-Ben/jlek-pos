using JLek.POS.Web.Presentation.Dtos;
using JLek.POS.Web.Presentation.Models;

namespace JLek.POS.Web.Presentation.Mapping;

public static class TableMapper
{
    public static TableViewModel ToViewModel(TableDto dto)
    {
        return new TableViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Status = dto.Status,
            IsAvailable = dto.Status == "Available",
            SeatCount = 0,
        };
    }
}