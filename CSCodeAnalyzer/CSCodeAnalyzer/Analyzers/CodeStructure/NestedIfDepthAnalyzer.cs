using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodeStructure
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NestedIfDepthAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.NestedIfCounterRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeIfStatement, SyntaxKind.IfStatement);
        }

        private void AnalyzeIfStatement(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is IfStatementSyntax ifStatement)
            {
                int currentIfDepth = 0;
                int maxDepth = 0;
                AnalyzeDepth(ifStatement, ref currentIfDepth, ref maxDepth);

                if (maxDepth > 3)
                {
                    var diagnostic = Diagnostic.Create(Rule, ifStatement.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private void AnalyzeDepth(IfStatementSyntax ifStatement, ref int currentIfDepth, ref int maxDepth)
        {
            ++currentIfDepth;

            if (currentIfDepth > maxDepth)
            {
                maxDepth = currentIfDepth;
            }

            if (ifStatement.Statement is BlockSyntax block)
            {
                foreach (var statement in block.Statements)
                {
                    if (statement is IfStatementSyntax nestedIf)
                    {
                        AnalyzeDepth(nestedIf, ref currentIfDepth, ref maxDepth);
                    }
                }
            }

            --currentIfDepth;
        }
    }
}
