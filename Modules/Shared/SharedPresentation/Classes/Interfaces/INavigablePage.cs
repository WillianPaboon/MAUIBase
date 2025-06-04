namespace SharedPresentation.Classes.Interfaces
{
    /// <summary>
    /// Represents a page that can be navigated to with optional parameters.
    /// </summary>
    public interface INavigablePage
    {
        /// <summary>
        /// Gets or sets the parameters associated with the page.
        /// </summary>
        object? Parameters { get; set; }
    }
}
