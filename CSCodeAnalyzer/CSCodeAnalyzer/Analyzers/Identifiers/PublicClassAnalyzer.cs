using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.Identifiers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PublicClassAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.PublicClassRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzePublicClasses, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzePublicClasses(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is ClassDeclarationSyntax classDeclaration)
            {
                if (!classDeclaration.Modifiers.Any(SyntaxKind.PublicKeyword))
                {
                    var diagnostic = Diagnostic.Create(
                        Rule,
                        classDeclaration.Identifier.GetLocation(),
                        classDeclaration.Identifier.Text
                    );

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

    }
}
