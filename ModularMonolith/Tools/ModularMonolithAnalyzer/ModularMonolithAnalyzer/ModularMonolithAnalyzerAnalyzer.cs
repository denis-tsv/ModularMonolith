using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ModularMonolithAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ModularMonolithAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        private readonly List<(string From, string To)> _wrongReferences = new List<(string, string)>
        {
            ( "UseCases", "DataAccess"),
            ( "UseCases", "Infrastructure.Implementation"),
            ( "UseCases", "DomainServices.Implementation"),

            ("Controllers", "DataAccess"),
            ("Controllers", "Infrastructure.Interfaces"),
            ("Controllers", "DomainServices.Interfaces"),
            ("Controllers", "Infrastructure.Implementation"),
            ("Controllers", "DomainServices.Implementation"),

            ("Infrastructure.Implementation", "DomainServices.Implementation"),

        };

        private readonly List<string> _modules = new List<string>
        {
            "Communication", "Order"
        };

        private const string Category = "InvalidReference";

        private static readonly DiagnosticDescriptor CrossLevelReference = new DiagnosticDescriptor("CrosLvl", "Cross-level reference", "Cross-level reference", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: "Cross-level reference");
        private static readonly DiagnosticDescriptor CrossModuleReference = new DiagnosticDescriptor("CrosModule", "Cross-module reference", "Cross-module reference", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: "Cross-module reference");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(CrossLevelReference, CrossModuleReference);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.UsingDirective);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var namespaceName = ((UsingDirectiveSyntax)context.Node).Name.ToString();
            var assemblyName = context.Compilation.AssemblyName;

            foreach (var layer in _wrongReferences)
            {
                if (assemblyName.Contains(layer.From) && namespaceName.Contains(layer.To))
                {
                    context.ReportDiagnostic(Diagnostic.Create(CrossLevelReference, context.Node.GetLocation()));
                }
            }

            for (int i = 0; i < _modules.Count; i++)
            {
                for (int j = 0; j < _modules.Count; j++)
                {
                    if (i == j) continue;

                    if (assemblyName.Contains(_modules[i]) && namespaceName.Contains(_modules[j]) && !namespaceName.Contains("Contract"))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(CrossModuleReference, context.Node.GetLocation()));
                    }
                }
            }
        }
    }
}
