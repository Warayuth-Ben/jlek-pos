using JLek.POS.Web.Presentation.Platform;

namespace JLek.POS.Web.Presentation.Cashier;

public record TableCardModel : BasePresentationModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public bool IsOccupied => Status == "Occupied";
    public bool IsAvailable => Status == "Available";
    public string StatusColor => Status switch
    {
        "Available" => "green",
        "Occupied" => "orange",
        _ => "gray"
    };
}

public record MenuCategoryModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record MenuItemModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public bool IsAvailable { get; init; }
}

public record OrderItemModel
{
    public Guid Id { get; init; }
    public Guid MenuItemId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
}

public record OrderPanelModel : BasePresentationModel
{
    public Guid Id { get; init; }
    public Guid? TableId { get; init; }
    public string Status { get; init; } = string.Empty;
    public decimal Total { get; init; }
    public List<OrderItemModel> Items { get; init; } = new();
    public string StatusColor => Status switch
    {
        "Draft" => "gray",
        "Confirmed" => "blue",
        "Completed" => "green",
        "Cancelled" => "red",
        _ => "gray"
    };
    public bool IsEditable => Status == "Draft";
    public bool CanConfirm => Status == "Draft" && Items.Count > 0;
    public string TotalText => Total.ToString("F2");
}

public record OrderSummaryModel
{
    public int ItemCount { get; init; }
    public decimal Total { get; init; }
    public string TotalText => Total.ToString("F2");
}