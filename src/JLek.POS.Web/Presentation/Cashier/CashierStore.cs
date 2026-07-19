using JLek.POS.Web.Presentation.Context;
using JLek.POS.Web.Presentation.Platform;
using JLek.POS.Web.Clients.Tables;
using JLek.POS.Web.Clients.Orders;
using JLek.POS.Web.Clients.Menu;
using JLek.POS.Web.Contracts.Tables;
using JLek.POS.Web.Contracts.Orders;
using JLek.POS.Web.Contracts.Menu;

namespace JLek.POS.Web.Presentation.Cashier;

public class CashierStore : IBaseWorkspaceStore
{
    private readonly ITableClient _tableClient;
    private readonly IOrderClient _orderClient;
    private readonly IMenuClient _menuClient;
    private readonly ContextStore _contextStore;

    public CashierStore(
        ITableClient tableClient,
        IOrderClient orderClient,
        IMenuClient menuClient,
        ContextStore contextStore)
    {
        _tableClient = tableClient;
        _orderClient = orderClient;
        _menuClient = menuClient;
        _contextStore = contextStore;
    }

    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }
    public event Action? StateChanged;

    public List<TableCardModel> Tables { get; private set; } = new();
    public List<MenuCategoryModel> MenuCategories { get; private set; } = new();
    public List<MenuItemModel> MenuItems { get; private set; } = new();
    public OrderPanelModel? CurrentOrder { get; private set; }
    public Guid? SelectedTableId { get; private set; }
    public TableCardModel? SelectedTable => Tables.FirstOrDefault(t => t.Id == SelectedTableId);

    public async Task InitializeAsync()
    {
        await RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        try
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = null;
            NotifyStateChanged();

            var tablesTask = LoadTablesAsync();
            var menuTask = LoadMenuAsync();
            await Task.WhenAll(tablesTask, menuTask);

            IsLoading = false;
            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    public async Task HandleEventAsync(object businessEvent)
    {
        await RefreshAsync();
    }

    public void Dispose() { }

    private void NotifyStateChanged() => StateChanged?.Invoke();

    private async Task LoadTablesAsync()
    {
        var tables = await _tableClient.GetAllAsync();
        Tables = tables.Select(MapToTableCard).ToList();
    }

    private async Task LoadMenuAsync()
    {
        var categories = await _menuClient.GetCategoriesAsync();
        var allProducts = await _menuClient.GetProductsAsync();

        MenuCategories = categories.Select(c => new MenuCategoryModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();

        MenuItems = allProducts.Select(p => new MenuItemModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            IsAvailable = p.IsAvailable
        }).ToList();
    }

    public async Task SelectTableAsync(Guid tableId)
    {
        SelectedTableId = tableId;
        var table = Tables.FirstOrDefault(t => t.Id == tableId);
        if (table is not null)
        {
            _contextStore.Select(new SelectionContext
            {
                EntityType = "Table",
                EntityId = tableId.ToString(),
                DisplayName = table.Name,
                Status = table.Status
            });
        }

        await LoadOrderForTableAsync(tableId);
        NotifyStateChanged();
    }

    public async Task OpenTableAsync(Guid tableId)
    {
        try
        {
            IsLoading = true;
            NotifyStateChanged();

            var tableResponse = await _tableClient.OpenAsync(tableId);
            var order = await _orderClient.CreateAsync(tableId);

            // Refresh tables
            await LoadTablesAsync();

            // Set selected table and order
            SelectedTableId = tableId;
            CurrentOrder = MapToOrderPanel(order);

            _contextStore.Select(new SelectionContext
            {
                EntityType = "Table",
                EntityId = tableId.ToString(),
                DisplayName = tableResponse.Name,
                Status = "Occupied"
            });

            IsLoading = false;
            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    public async Task AddMenuItemAsync(Guid menuItemId, int quantity = 1)
    {
        if (CurrentOrder is null || SelectedTableId is null) return;

        try
        {
            var menuItem = MenuItems.FirstOrDefault(m => m.Id == menuItemId);
            if (menuItem is null) return;

            var updatedOrder = await _orderClient.AddItemAsync(CurrentOrder.Id, menuItemId, quantity, menuItem.Price);
            CurrentOrder = MapToOrderPanel(updatedOrder);
            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
            NotifyStateChanged();
        }
    }

    public async Task ConfirmOrderAsync()
    {
        if (CurrentOrder is null) return;

        try
        {
            var confirmed = await _orderClient.ConfirmAsync(CurrentOrder.Id);
            CurrentOrder = MapToOrderPanel(confirmed);
            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
            NotifyStateChanged();
        }
    }

    public void ClearSelection()
    {
        SelectedTableId = null;
        CurrentOrder = null;
        _contextStore.ClearSelection();
        NotifyStateChanged();
    }

    private async Task LoadOrderForTableAsync(Guid tableId)
    {
        // Find active order for this table
        var orders = await _orderClient.GetAllAsync();
        var activeOrder = orders.FirstOrDefault(o => o.TableId == tableId && (o.Status == "Draft" || o.Status == "Confirmed"));
        CurrentOrder = activeOrder is not null ? MapToOrderPanel(activeOrder) : null;
    }

    private static TableCardModel MapToTableCard(TableResponse t) => new()
    {
        Id = t.Id,
        Name = t.Name,
        Status = t.Status,
        IsLoading = false
    };

    private static OrderPanelModel MapToOrderPanel(OrderResponse o)
    {
        var items = o.Items?.Select(i => new OrderItemModel
        {
            Id = i.Id,
            MenuItemId = i.MenuItemId,
            Name = $"Item {i.MenuItemId.ToString()[..4]}",
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            TotalPrice = i.TotalPrice
        }).ToList() ?? new();

        return new OrderPanelModel
        {
            Id = o.Id,
            TableId = o.TableId,
            Status = o.Status,
            Total = o.Total,
            Items = items,
            IsLoading = false
        };
    }
}