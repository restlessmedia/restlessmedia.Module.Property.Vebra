using System;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiPropertyTokenEventArgs : EventArgs
  {
    public ApiPropertyTokenEventArgs(string token)
    {
      Token = token;
    }

    public string Token { get; set; }
  }
}