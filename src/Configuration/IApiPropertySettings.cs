namespace restlessmedia.Module.Property.Vebra.Configuration
{
  public interface IApiPropertySettings
  {
    string Url { get; }

    /// <summary>
    /// Sql connection timeout
    /// </summary>
    int Timeout { get; }
  }
}