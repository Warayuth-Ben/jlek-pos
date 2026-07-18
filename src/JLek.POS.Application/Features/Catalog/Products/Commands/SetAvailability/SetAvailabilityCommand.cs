using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.SetAvailability;

public sealed record SetAvailabilityCommand(
    ProductId ProductId,
    ProductStatus Status);