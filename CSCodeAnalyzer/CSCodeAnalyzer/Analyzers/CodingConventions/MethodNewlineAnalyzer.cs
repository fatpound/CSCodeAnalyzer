using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MethodNewlineAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.MethodNewLineRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeMethodNewline, SyntaxKind.MethodDeclaration);
        }

        public static void AnalyzeMethodNewline(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is MethodDeclarationSyntax methodDeclaration)
            {
                var methodSyntax = methodDeclaration.Parent.ChildNodes().OfType<MethodDeclarationSyntax>().ToList();

                int currentIndex = methodSyntax.IndexOf(methodDeclaration);

                for (int i = currentIndex + 1; i < methodSyntax.Count; i++)
                {
                    var nextMethod = methodSyntax[i];

                    var currentMethodEndLine = methodDeclaration.GetLocation().GetLineSpan().EndLinePosition.Line;
                    var nextMethodStartLine = nextMethod.GetLocation().GetLineSpan().StartLinePosition.Line;

                    if (nextMethodStartLine - currentMethodEndLine <= 1)
                    {
                        var diagnostic = Diagnostic.Create(
                            Rule,
                            nextMethod.Identifier.GetLocation(),
                            nextMethod.Identifier
                        );

                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
