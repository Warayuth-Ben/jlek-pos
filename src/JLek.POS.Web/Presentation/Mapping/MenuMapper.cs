using JLek.POS.Web.Presentation.Dtos;
using JLek.POS.Web.Presentation.Models;

namespace JLek.POS.Web.Presentation.Mapping;

public static class MenuMapper
{
    public static MenuItemViewModel ToViewModel(MenuItemDto dto)
    {
        return new MenuItemViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            Available = true,
        };
    }
}