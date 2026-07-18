namespace JLek.POS.Web.Contracts.Tables;

public sealed record TableResponse(
    Guid Id,
    string Name,
    string Status,
    Guid? ActiveSessionId);