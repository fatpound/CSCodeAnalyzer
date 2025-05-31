using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LineLengthAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.LineLengthRule;
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxTreeAction(AnalyzeLineLength);
        }

        private static void AnalyzeLineLength(SyntaxTreeAnalysisContext context)
        {
            var root = context.Tree.GetRoot(context.CancellationToken);
            var lines = root.ToFullString().Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Length > 120)
                {
                    var diagnostic = Diagnostic.Create(
                        Rule,
                        Location.Create(context.Tree,
                        new TextSpan(root.GetLocation().SourceSpan.Start + i, line.Length)), i + 1
                    );

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

    }
}