namespace JLek.POS.Web.Presentation.Context;

/// <summary>
/// Types of context in the Presentation layer.
/// </summary>
public enum ContextType
{
    Global,
    Workspace,
    Selection,
    Transient
}

/// <summary>
/// Represents an active context item (e.g., selected table, selected order).
/// </summary>
public record SelectionContext
{
    public string EntityType { get; init; } = string.Empty;
    public string EntityId { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public string? Status { get; init; }
}

/// <summary>
/// Manages context across workspace boundaries.
/// </summary>
public class ContextStore
{
    private SelectionContext? _currentSelection;

    public SelectionContext? CurrentSelection => _currentSelection;

    public event Action<SelectionContext?>? SelectionChanged;

    public void Select(SelectionContext context)
    {
        _currentSelection = context;
        SelectionChanged?.Invoke(context);
    }

    public void ClearSelection()
    {
        _currentSelection = null;
        SelectionChanged?.Invoke(null);
    }
}

/// <summary>
/// Tracks the currently active workspace.
/// </summary>
public class WorkspaceContext
{
    private string _currentWorkspace = "Home";

    public string CurrentWorkspace => _currentWorkspace;
    public string? PreviousWorkspace { get; private set; }

    public event Action<string>? WorkspaceChanged;

    public void NavigateTo(string workspace)
    {
        PreviousWorkspace = _currentWorkspace;
        _currentWorkspace = workspace;
        WorkspaceChanged?.Invoke(workspace);
    }

    public void NavigateBack()
    {
        if (PreviousWorkspace is not null)
        {
            NavigateTo(PreviousWorkspace);
        }
    }
}