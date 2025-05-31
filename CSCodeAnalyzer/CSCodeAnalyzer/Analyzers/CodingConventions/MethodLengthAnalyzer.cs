using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MethodLengthAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.MethodLengthRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeMethodLength, SyntaxKind.MethodDeclaration);
        }

        public static void AnalyzeMethodLength(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is MethodDeclarationSyntax methodDeclaration)
            {
                var startLine = methodDeclaration.GetLocation().GetLineSpan().StartLinePosition.Line;
                var endLine   = methodDeclaration.GetLocation().GetLineSpan().EndLinePosition.Line;

                if (endLine - startLine > 30)
                {
                    var diagnostic = Diagnostic.Create(
                        Rule,
                        methodDeclaration.Identifier.GetLocation(),
                        methodDeclaration.Identifier.Text
                    );

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
