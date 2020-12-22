using restlessmedia.Module.File;

namespace restlessmedia.Module.Property.Vebra
{
  public class EPCGraph : Resource
  {
    public override FileFlags? Flags
    {
      get
      {
        return FileFlags.EPCGraph;
      }
    }
  }
}