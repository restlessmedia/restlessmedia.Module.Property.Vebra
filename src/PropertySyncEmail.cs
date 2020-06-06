using restlessmedia.Module.Email;
using restlessmedia.Module.Security;
using System;

namespace restlessmedia.Module.Property.Vebra
{
  public class SyncEmail : AdminEmail
  {
    public SyncEmail(IEmailContext emailContext, ISyncReview review, IUserInfo user = null)
      : base(emailContext)
    {
      _review = review ?? throw new ArgumentNullException(nameof(review));

      if (user != null)
      {
        AddLine($"Sync initiated by {user.Email}");
      }

      string summary = _review.GetSummary();

      if (!string.IsNullOrWhiteSpace(summary))
      {
        AddLine(summary);
      }
    }

    public override string Subject
    {
      get
      {
        return string.Concat(EmailContext.LicenseSettings.CompanyName, ": ", _review.Title);
      }
    }

    public override IAttachment[] Attachments
    {
      get
      {
        return _review.Attachments;
      }
    }

    private readonly ISyncReview _review;
  }
}