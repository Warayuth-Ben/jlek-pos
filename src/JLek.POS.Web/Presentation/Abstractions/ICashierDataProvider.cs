using JLek.POS.Web.Presentation.Dtos;

namespace JLek.POS.Web.Presentation.Abstractions;

public interface ICashierDataProvider
{
    Task<CashierDataDto> LoadAsync(CancellationToken cancellationToken = default);
}
