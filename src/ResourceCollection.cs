using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace restlessmedia.Module.Property.Vebra
{
  public class ResourceCollection : IEnumerable<Resource>
  {
    public ResourceCollection(IEnumerable<Resource> resources)
    {
      _items = resources != null ? resources.ToList() : new List<Resource>(0);
    }

    public int IndexOf(Resource resource)
    {
      return _items.IndexOf(resource);
    }

    public IEnumerator<Resource> GetEnumerator()
    {
      return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    private readonly IList<Resource> _items;
  }
}