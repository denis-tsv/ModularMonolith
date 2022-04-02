using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Xunit;

namespace Shop.Tests.Unit
{
    public class ArchitectureTests
    {
        [Fact]
        public void CrossLayerReferences()
        {
            var wrongReferences = new List<(string From, string To)>
            {
                ("UseCases", "DataAccess.MsSql"),
                ("UseCases", "Infrastructure.Implementation"),
                ("UseCases", "DomainServices.Implementation"),

                ("Controllers", "DataAccess"),
                ("Controllers", "Infrastructure.Interfaces"),
                ("Controllers", "DomainServices.Interfaces"),
                ("Controllers", "Infrastructure.Implementation"),
                ("Controllers", "DomainServices.Implementation"),

                ("Infrastructure.Implementation", "DomainServices.Implementation"),
            };
            
            var location = Assembly.GetExecutingAssembly().Location;
            var assemblies = Directory.EnumerateFiles(Path.GetDirectoryName(location), "Shop*.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToList();

            foreach (var layer in wrongReferences)
            {
                foreach (var assembly in assemblies)
                {
                    foreach (var reference in assembly.GetReferencedAssemblies())
                    {
                        Assert.False(assembly.FullName.Contains(layer.From) && reference.FullName.Contains(layer.To),
                            $"Cross-layer reference from '{assembly.FullName}' to '{reference.FullName}'");
                    }
                }
            }
        }

        [Fact]
        public void CrossModuleReferences()
        {
            var modules = new List<string>
            {
                "Communication", "Order"
            };

            var location = Assembly.GetExecutingAssembly().Location;
            var assemblies = Directory.EnumerateFiles(Path.GetDirectoryName(location), "Shop*.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToList();

            foreach (var fromModule in modules)
            {
                foreach (var toModule in modules)
                {
                    if (fromModule == toModule) continue;

                    foreach (var assembly in assemblies)
                    {
                        foreach (var reference in assembly.GetReferencedAssemblies())
                        {
                            //not only reference, but real usage of class from other assembly
                            Assert.False(assembly.FullName.Contains(fromModule) && 
                                         reference.FullName.Contains(toModule) && 
                                         !reference.FullName.Contains("Contract"),
                                $"Cross-context reference from '{assembly.FullName}' to '{reference.FullName}'");
                        }
                    }
                }
            }
        }
    }
}
