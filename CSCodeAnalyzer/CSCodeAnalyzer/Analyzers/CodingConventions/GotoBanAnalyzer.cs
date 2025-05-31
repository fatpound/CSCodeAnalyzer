using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class GotoBanAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.GotoBanRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeGoto, SyntaxKind.GotoStatement);
        }

        public static void AnalyzeGoto(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is GotoStatementSyntax gotoStatement)
            {
                var targetLabel = gotoStatement.Expression.ToString();

                var diagnostic = Diagnostic.Create(Rule, context.Node.GetLocation(), targetLabel);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
