﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Resources;
using HFAnalyzer;

namespace CodeAnalyzer.Analyzers.CodingConventions
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    class PublicPropertyAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = DiagnosticRules.PublicClassRule;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzePublicProperty, SyntaxKind.PropertyDeclaration);
        }

        private static void AnalyzePublicProperty(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is PropertyDeclarationSyntax propertyDeclaration)
            {
                if (!propertyDeclaration.Modifiers.Any(SyntaxKind.PublicKeyword))
                {
                    var diagnostic = Diagnostic.Create(DiagnosticRules.PublicPropertyRule
                                                      , propertyDeclaration.GetLocation()
                                                      , propertyDeclaration.Identifier.Text);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
