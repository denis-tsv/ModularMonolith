using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using NetArchTest.Rules;
using Xunit;

namespace Shop.Tests.Unit
{
    public class ArchitectureTests
    {
        private readonly string[] _modules = {"Communication", "Order"};
        [Fact]
        public void Use_Cases_Not_Refers_To_DataAccess()
        {
            foreach (var module in _modules)
            {
                var result = Types.InCurrentDomain()
                    .That()
                    .ResideInNamespaceContaining($"Shop.{module}.UseCases")
                    .Should()
                    .NotHaveDependencyOn($"Shop.{module}.DataAccess")
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void CrossLayerReferences()
        {
            var wrongReferences = new List<(string From, string To)>
            {
                ("UseCases", "DataAccess"),
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
            var location = Assembly.GetExecutingAssembly().Location;
            var assemblies = Directory.EnumerateFiles(Path.GetDirectoryName(location), "Shop*.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToList();

            for (int i = 0; i < _modules.Length; i++)
            {
                for (int j = 0; j < _modules.Length; j++)
                {
                    if (i == j) continue;

                    foreach (var assembly in assemblies)
                    {
                        foreach (var reference in assembly.GetReferencedAssemblies())
                        {
                            //not only reference, but real usage of class from other assembly
                            Assert.False(assembly.FullName.Contains(_modules[i]) && 
                                         reference.FullName.Contains(_modules[j]) && 
                                         !reference.FullName.Contains("Contract"),
                                $"Cross-context reference from '{assembly.FullName}' to '{reference.FullName}'");
                        }
                    }
                }
            }
        }
    }
}
