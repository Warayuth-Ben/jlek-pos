using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Commands.AssignTable;

public sealed record AssignTableCommand(
    TableId TableId,
    OrderSessionId SessionId);