using restlessmedia.Module.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace restlessmedia.Module.Property.Vebra
{
  public class PropertySyncReview : ISyncReview
  {
    public PropertySyncReview()
    {
      Exceptions = new List<Exception>();
      Messages = new List<string>();
    }

    public string Title
    {
      get { return "Property sync results"; }
    }

    public bool Completed
    {
      get
      {
        return true;
      }
    }

    public string GetSummary()
    {
      StringBuilder builder = new StringBuilder();

      if (Messages.Any())
      {
        builder.AppendLine("Messages:");

        foreach(string messaage in Messages)
        {
          builder.AppendLine(messaage);
        }

        builder.AppendLine();
      }

      if (Exceptions.Any())
      {
        builder.AppendLine("Errors:");

        foreach (Exception exception in Exceptions)
        {
          builder.AppendLine(exception.Message);
        }
      }

      return builder.ToString();
    }

    public IAttachment[] Attachments
    {
      get
      {
        return null;
      }
    }

    public ApiProperties Properties { get; set; }

    public readonly IList<Exception> Exceptions;

    public readonly IList<string> Messages;
  }
}