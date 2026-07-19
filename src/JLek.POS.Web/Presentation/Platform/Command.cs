namespace JLek.POS.Web.Presentation.Platform;

/// <summary>
/// Represents a user action dispatched from the UI to the Application layer.
/// </summary>
public record Command
{
    public string Type { get; init; } = string.Empty;
    public object? Payload { get; init; }
    public Func<Task>? OnSuccess { get; init; }
    public Func<string, Task>? OnError { get; init; }
}

/// <summary>
/// Result of executing a command.
/// </summary>
public record CommandResult
{
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }

    public static CommandResult Success() => new() { IsSuccess = true };
    public static CommandResult Failure(string message) => new() { IsSuccess = false, ErrorMessage = message };
}

/// <summary>
/// Dispatches commands to the application layer.
/// </summary>
public class CommandDispatcher
{
    private readonly Func<Command, Task<CommandResult>> _execute;

    public CommandDispatcher(Func<Command, Task<CommandResult>> execute)
    {
        _execute = execute;
    }

    public async Task<CommandResult> DispatchAsync(Command command)
    {
        var result = await _execute(command);
        if (result.IsSuccess)
        {
            if (command.OnSuccess is not null)
                await command.OnSuccess();
        }
        else
        {
            if (command.OnError is not null)
                await command.OnError(result.ErrorMessage ?? "Unknown error");
        }
        return result;
    }
}