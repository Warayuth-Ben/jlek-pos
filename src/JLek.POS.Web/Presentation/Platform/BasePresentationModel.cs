namespace JLek.POS.Web.Presentation.Platform;

/// <summary>
/// Base class for all Presentation Models.
/// Presentation Models are immutable objects that transform API DTOs into UI-optimized data.
/// They are the only data type that Business Components consume.
/// </summary>
public abstract record BasePresentationModel
{
    /// <summary>
    /// Whether this model is in a loading state.
    /// </summary>
    public bool IsLoading { get; init; }

    /// <summary>
    /// Whether this model encountered an error during load.
    /// </summary>
    public bool HasError { get; init; }

    /// <summary>
    /// Error message if HasError is true.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Whether this model is empty (no data).
    /// </summary>
    public bool IsEmpty { get; init; }
}