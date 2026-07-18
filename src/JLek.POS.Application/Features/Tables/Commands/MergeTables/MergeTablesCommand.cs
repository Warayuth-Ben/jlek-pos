using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Commands.MergeTables;

public sealed record MergeTablesCommand(
    TableId PrimaryTableId,
    TableId TableToMergeId);