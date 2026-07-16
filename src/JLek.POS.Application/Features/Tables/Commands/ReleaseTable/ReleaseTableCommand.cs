using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Commands.ReleaseTable;

public sealed record ReleaseTableCommand(
    TableId TableId);