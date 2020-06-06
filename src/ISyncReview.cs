using restlessmedia.Module.Email;

namespace restlessmedia.Module.Property.Vebra
{
  public interface ISyncReview
  {
    string Title { get; }

    bool Completed { get; }

    IAttachment[] Attachments { get; }

    string GetSummary();
  }
}