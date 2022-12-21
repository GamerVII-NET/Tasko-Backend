namespace Tasko.Client.Models;

public class ValidationFailure
{
    /// <summary>
	/// The name of the property.
	/// </summary>
	public string PropertyName { get; set; }

    /// <summary>
    /// The error message
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// The property value that caused the failure.
    /// </summary>
    public object AttemptedValue { get; set; }

    /// <summary>
    /// Custom state associated with the failure.
    /// </summary>
    public object CustomState { get; set; }

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the formatted message placeholder values.
    /// </summary>
    public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; }
}