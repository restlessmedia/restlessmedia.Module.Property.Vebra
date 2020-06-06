using restlessmedia.Module.File;

namespace restlessmedia.Module.Property.Vebra
{
  public class Floorplan : Resource
  {
    public override FileFlags? Flags
    {
      get
      {
        return FileFlags.FloorPlan;
      }
    }
  }
}