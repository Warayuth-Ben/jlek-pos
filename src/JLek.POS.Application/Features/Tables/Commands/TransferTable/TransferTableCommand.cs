using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Commands.TransferTable;

public sealed record TransferTableCommand(
    TableId SourceTableId,
    TableId DestinationTableId);