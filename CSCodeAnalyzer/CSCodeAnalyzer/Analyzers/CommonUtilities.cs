using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class CommonUtilities
{
    public static bool IsPascalCase(string identifier)
    {
        return Regex.IsMatch(identifier, @"^[A-Z][a-z0-9]*(?:_[A-Z][a-z0-9]*)*$");
    }

    public static bool IsCamelCase(string identifier)
    {
        return Regex.IsMatch(identifier, @"^[a-z]+(?:[A-Z][a-z0-9])$");
    }

    public static bool ContainsOnlyWhitespace(SyntaxNode Node)
    {
        return Node.DescendantTrivia().All(trivia => trivia.IsKind(SyntaxKind.WhitespaceTrivia));
    }

    public static bool IsValidIndentation(SyntaxNode Node, int spacesPerIndentation = 4)
    {
        var leadingTrivia = Node.GetLeadingTrivia();
        var spaceCount = leadingTrivia.Count(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        return spaceCount % spacesPerIndentation == 0;
    }

    public static bool IsIdentifierUsedInCode(SyntaxNode root, string identifier)
    {
        return root.DescendantNodes().OfType<IdentifierNameSyntax>().Any(id => id.Identifier.Text == identifier);
    }
}
