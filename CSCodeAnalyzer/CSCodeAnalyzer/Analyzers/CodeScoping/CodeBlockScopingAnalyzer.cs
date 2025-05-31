using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodeScoping
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CodeBlockScopingAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.CodeBlockScopingRule;
        
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeCodeBlockScoping, SyntaxKind.Block);
        }

        private static void AnalyzeCodeBlockScoping(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is BlockSyntax block)
            {
                var statements = block.Statements;
                if (statements.Count == 1 && statements.First() is BlockSyntax)
                {
                    var diagnostic = Diagnostic.Create(Rule
                                                      , block.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
