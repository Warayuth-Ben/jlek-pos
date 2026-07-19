namespace JLek.POS.Web.Presentation.Platform;

/// <summary>
/// Base interface for all Workspace Stores.
/// Every workspace owns exactly one Store that manages all Presentation Models.
/// </summary>
public interface IBaseWorkspaceStore
{
    /// <summary>
    /// Whether the store is currently loading data.
    /// </summary>
    bool IsLoading { get; }

    /// <summary>
    /// Whether the store encountered an error.
    /// </summary>
    bool HasError { get; }

    /// <summary>
    /// Error message if HasError is true.
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// Event raised when store state changes.
    /// </summary>
    event Action? StateChanged;

    /// <summary>
    /// Initialize the store, fetching initial data.
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Refresh all data in the store.
    /// </summary>
    Task RefreshAsync();

    /// <summary>
    /// Handle a business event that affects this store.
    /// </summary>
    Task HandleEventAsync(object businessEvent);

    /// <summary>
    /// Dispose the store, cleaning up subscriptions.
    /// </summary>
    void Dispose();
}