using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace restlessmedia.Module.Property.Vebra.Tests
{
  [AttributeUsage(AttributeTargets.Method)]
  internal class ResourceInlineData : DataAttribute
  {
    public ResourceInlineData(string resourceName)
    {
      _resourceName = resourceName;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
      return new[] { new[] { GetResourceStream() } };
    }

    private Stream GetResourceStream()
    {
      return Assembly.GetExecutingAssembly().GetManifestResourceStream(_resourceName);
    }

    private readonly string _resourceName;
  }
}