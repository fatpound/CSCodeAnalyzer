using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.Identifiers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PublicPropertyAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.PublicClassRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzePublicProperty, SyntaxKind.PropertyDeclaration);
        }

        private static void AnalyzePublicProperty(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is PropertyDeclarationSyntax propertyDeclaration)
            {
                if (!propertyDeclaration.Modifiers.Any(SyntaxKind.PublicKeyword))
                {
                    var diagnostic = Diagnostic.Create(
                        Diagnostics.PublicPropertyRule,
                        propertyDeclaration.GetLocation(),
                        propertyDeclaration.Identifier.Text
                    );

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
