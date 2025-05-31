using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodeScoping
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ProperEnclosementAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.ProperEnclosementRule;
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxTreeAction(AnalyzeSyntaxTree);
        }

        private void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            var root = context.Tree.GetRoot(context.CancellationToken);
            AnalyzeParentheses(root, context);
        }

        private void AnalyzeParentheses(SyntaxNode node, SyntaxTreeAnalysisContext context)
        {
            foreach (var childNode in node.DescendantNodes())
            {
                if (childNode.IsKind(SyntaxKind.ParenthesizedExpression))
                {
                    var parenthesizedExpression = (ParenthesizedExpressionSyntax)childNode;

                    var openParenToken  = parenthesizedExpression.OpenParenToken;
                    var closeParenToken = parenthesizedExpression.CloseParenToken;

                    // Check for unclosed parentheses
                    //
                    if (openParenToken.IsMissing && !closeParenToken.IsMissing)
                    {
                        var diagnostic = Diagnostic.Create(Rule, closeParenToken.GetLocation(), "Unclosed parenthesis");
                        context.ReportDiagnostic(diagnostic);
                    }

                    // Check for unopened parentheses
                    //
                    if (!openParenToken.IsMissing && closeParenToken.IsMissing)
                    {
                        var diagnostic = Diagnostic.Create(Rule, openParenToken.GetLocation(), "Unopened parenthesis");
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
