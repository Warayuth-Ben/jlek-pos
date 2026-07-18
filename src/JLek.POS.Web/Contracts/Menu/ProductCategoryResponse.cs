namespace JLek.POS.Web.Contracts.Menu;

public sealed record ProductCategoryResponse(
    Guid Id,
    string Name,
    int SortOrder);

public sealed record ProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    bool IsAvailable);

public sealed record ProductCategoryWithProducts(
    ProductCategoryResponse Category,
    List<ProductResponse> Products);