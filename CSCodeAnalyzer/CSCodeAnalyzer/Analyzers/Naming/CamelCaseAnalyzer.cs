using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace CSCodeAnalyzer.Analyzers.Naming
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CamelCaseAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Diagnostics.CamelCaseRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeCamelCase, SyntaxKind.VariableDeclaration);
            // context.RegisterSyntaxNodeAction(AnalyzeCamelCase, SyntaxKind.FieldDeclaration);
        }

        public static void AnalyzeCamelCase(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is VariableDeclarationSyntax variableDeclaration)
            {
                foreach (var variable in variableDeclaration.Variables)
                {
                    var variableName = variable.Identifier.Text;

                    if (variableName.Length > 3 && CommonUtilities.IsCamelCase(variableName) == false)
                    {
                        if (! Regex.IsMatch(variableName, "^[a-z]+$"))
                        {
                            var diagnostic = Diagnostic.Create(Rule, variable.Identifier.GetLocation(), variable.Identifier.Text, variableName);
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
            else if (context.Node is FieldDeclarationSyntax fieldDeclaration)
            {
                foreach (var field in fieldDeclaration.Declaration.Variables)
                {
                    var fieldName = field.Identifier.Text;

                    if (fieldName.Length > 3 && CommonUtilities.IsCamelCase(fieldName) == false)
                    {
                        if (! Regex.IsMatch(fieldName, "^[a-z]+$"))
                        {
                            var diagnostic = Diagnostic.Create(Rule, field.Identifier.GetLocation(), field.Identifier.Text, fieldName);
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }
    }
}
