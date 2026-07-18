using JLek.POS.Web.Contracts.Tables;

namespace JLek.POS.Web.Clients.Tables;

public interface ITableClient
{
    Task<List<TableResponse>> GetAllAsync(CancellationToken ct = default);
    Task<List<TableResponse>> GetAvailableAsync(CancellationToken ct = default);
    Task<TableResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<TableResponse> OpenAsync(Guid id, CancellationToken ct = default);
    Task<TableResponse> ReleaseAsync(Guid id, CancellationToken ct = default);
}
