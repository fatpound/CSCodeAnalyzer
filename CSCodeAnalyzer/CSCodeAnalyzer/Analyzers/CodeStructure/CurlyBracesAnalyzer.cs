using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodeStructure
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CurlyBracesAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.CurlyBracesRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {   
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeIfStatement, SyntaxKind.IfStatement);
            context.RegisterSyntaxNodeAction(AnalyzeIfStatement, SyntaxKind.ElseClause);
            context.RegisterSyntaxNodeAction(AnalyzeSingleStatement, SyntaxKind.ForStatement);
            context.RegisterSyntaxNodeAction(AnalyzeSingleStatement, SyntaxKind.WhileStatement);
        }

        private void AnalyzeIfStatement(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is IfStatementSyntax ifStatement)
            {
                if (ifStatement.Statement is BlockSyntax)
                {

                }
                else
                {
                    var diagnostic = Diagnostic.Create(Rule, ifStatement.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else if (context.Node is ElseClauseSyntax elseClause)
            {
                if (elseClause.Statement is BlockSyntax)
                {

                }
                else
                {
                    var diagnostic = Diagnostic.Create(Rule, elseClause.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private void AnalyzeSingleStatement(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is StatementSyntax statement && !(statement is BlockSyntax))
            {
                var diagnostic = Diagnostic.Create(Rule, statement.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
