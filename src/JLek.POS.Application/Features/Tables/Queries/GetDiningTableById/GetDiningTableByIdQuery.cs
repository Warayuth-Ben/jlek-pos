using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Queries.GetDiningTableById;

public sealed record GetDiningTableByIdQuery(
    TableId TableId);