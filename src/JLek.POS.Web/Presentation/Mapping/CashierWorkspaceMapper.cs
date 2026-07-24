using JLek.POS.Web.Presentation.Dtos;
using JLek.POS.Web.Presentation.Models;

namespace JLek.POS.Web.Presentation.Mapping;

public static class CashierWorkspaceMapper
{
    public static CashierWorkspaceViewModel ToViewModel(CashierDataDto dto)
    {
        return new CashierWorkspaceViewModel
        {
            Tables = dto.Tables.Select(TableMapper.ToViewModel).ToList(),
            Menu = dto.MenuItems.Select(MenuMapper.ToViewModel).ToList(),
        };
    }
}