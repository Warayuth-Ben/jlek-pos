using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Commands.OpenTable;

public sealed record OpenTableCommand(
    TableId TableId);