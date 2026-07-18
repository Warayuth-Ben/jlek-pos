using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Commands.SplitTable;

public sealed record SplitTableCommand(
    TableId PrimaryTableId,
    TableId TableToSplitId);