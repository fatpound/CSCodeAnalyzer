using HFAnalyzer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;
using System.Resources;


namespace CodeAnalyzer.Analyzers.Naming
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConstantAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = HFAnalyzer.DiagnosticRules.PascalCaseRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeConstantNaming, SyntaxKind.FieldDeclaration);
        }

        private static void AnalyzeConstantNaming(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is FieldDeclarationSyntax fieldDeclaration)
            {
                if (fieldDeclaration.Modifiers.Any(SyntaxKind.ConstKeyword))
                {
                    foreach (var variable in fieldDeclaration.Declaration.Variables)
                    {
                        var constantName = variable.Identifier.Text;
                        if (!constantName.All(char.IsUpper) || !constantName.Contains('_'))
                        {
                            var diagnostic = Diagnostic.Create(Rule, variable.Identifier.GetLocation(), constantName);

                            context.ReportDiagnostic(diagnostic);

                        }
                    }
                }
            }
        }
    }
}
