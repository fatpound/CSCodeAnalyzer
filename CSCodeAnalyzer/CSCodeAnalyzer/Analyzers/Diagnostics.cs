using Microsoft.CodeAnalysis;

namespace CSCodeAnalyzer
{
    public static class Diagnostics
    {
        // Naming

        public static readonly DiagnosticDescriptor PascalCaseRule        = new DiagnosticDescriptor(
            id:                 "HF001",
            title:              "PascalCase Naming Rule",
            messageFormat:      "The name '{0}' should be in PascalCase",
            category:           "Naming",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor CamelCaseRule         = new DiagnosticDescriptor(
            id:                 "HF002",
            title:              "CamelCase Naming Rule",
            messageFormat:      "The name '{0}' should be in camelCase",
            category:           "Naming",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor ConstantsRule         = new DiagnosticDescriptor(
            id:                 "HF003",
            title:              "ConstantsRule",
            messageFormat:      "The constant '{0}' is not properly named",
            category:           "Naming",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor IndentationRule       = new DiagnosticDescriptor(
            id:                 "HF004",
            title:              "Indentation Rule",
            messageFormat:      "Line {0}: Use four spaces",
            category:           "Naming",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );

        // CodingConventions

        public static readonly DiagnosticDescriptor LineLengthRule        = new DiagnosticDescriptor(
            id:                 "HF005",
            title:              "Line Length Rule",
            messageFormat:      "A line should not exceed 120 characters",
            category:           "CodingConventions",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor MethodNewLineRule     = new DiagnosticDescriptor(
            id:                 "HF006",
            title:              "New Line Rule",
            messageFormat:      "You should add a new line between methods",
            category:           "CodingConventions",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor MethodLengthRule      = new DiagnosticDescriptor(
           id:                 "HF007",
           title:              "Method Length Rule",
           messageFormat:      "Method '{0}' should not exceed ~30 lines",
           category:           "CodingConventions",
           defaultSeverity:    DiagnosticSeverity.Info,
           isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor GotoBanRule           = new DiagnosticDescriptor(
            id:                 "HF008",
            title:              "Goto Ban Rule",
            messageFormat:      "Goto usage is considered a bad practice",
            category:           "CodingConventions",
            defaultSeverity:    DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor VarBanRule            = new DiagnosticDescriptor(
            id:                 "HF009",
            title:              "Var Ban Rule",
            messageFormat:      "Using var instead of type is considered a bad practice",
            category:           "CodingConventions",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );

        // CodeStructure

        public static readonly DiagnosticDescriptor ParameterCountRule    = new DiagnosticDescriptor(
            id:                 "HF009",
            title:              "Parameter Count Rule",
            messageFormat:      "You shouldn't use more than five parameters per method",
            category:           "CodeStructure",
            defaultSeverity:    DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor NestedIfCounterRule   = new DiagnosticDescriptor(
            id:                 "HF010",
            title:              "Detect Nested If Statements",
            messageFormat:      "Nested if statements detected. Maximum allowed depth is 3.",
            category:           "CodeStructure",
            defaultSeverity:    DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: "This analyzer detects nested if statements and differentiates them from same level if statements."
        );
        public static readonly DiagnosticDescriptor CurlyBracesRule       = new DiagnosticDescriptor(
            id:                 "HF011",
            title:              "Unnecessary Curly Braces Rule",
            messageFormat:      "You've entered unnecessary curly braces",
            category:           "CodeStructure",
            defaultSeverity:    DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        // Identifiers

        public static readonly DiagnosticDescriptor PublicClassRule       = new DiagnosticDescriptor(
            id:                 "HF012",
            title:              "Public Class Access Modifier Rule",
            messageFormat:      "Class '{0}' should be public",
            category:           "Identifiers",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor PrivateMethodRule     = new DiagnosticDescriptor(
            id:                 "HF013",
            title:              "Private Method Modifier Rule",
            messageFormat:      "Method '{0}' should be private",
            category:           "Identifiers",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor PublicPropertyRule    = new DiagnosticDescriptor(
            id:                 "HF014",
            title:              "Public Property Modifier Rule",
            messageFormat:      "Property '{0}' should be public",
            category:           "Identifiers",
            defaultSeverity:    DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );

        // CodeScoping

        public static readonly DiagnosticDescriptor CodeBlockScopingRule  = new DiagnosticDescriptor(
            id:                 "HF015",
            title:              "Code Block Framing Rule",
            messageFormat:      "This block is framed unnecessarily",
            category:           "CodeScoping",
            defaultSeverity:    DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor NullReferenceRule     = new DiagnosticDescriptor(
            id:                 "HF016",
            title:              "Null Reference Rule",
            messageFormat:      "You are trying to use something that is null",
            category:           "CodeScoping",
            defaultSeverity:    DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor ProperEnclosementRule = new DiagnosticDescriptor(
            id:                 "HF017",
            title:              "Proper Enclosement Rule",
            messageFormat:      "You're missing an enclosement",
            category:           "CodeScoping",
            defaultSeverity:    DiagnosticSeverity.Error,
            isEnabledByDefault: true
            );

        // Encapsulation

        public static readonly DiagnosticDescriptor GetterSetterRule      = new DiagnosticDescriptor(
            id:                 "HF018",
            title:              "Getter/Setter Rule",
            messageFormat:      "You're missing getter/setter method",
            category:           "Encapsulation",
            defaultSeverity:    DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );
    }
}
