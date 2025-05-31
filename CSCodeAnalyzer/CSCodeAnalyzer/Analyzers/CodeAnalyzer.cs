using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Linq;

namespace CSCodeAnalyzer
{
    public class CodeAnalyzer
    {
        public static Diagnostic[] GetSortedDiagnostics(DiagnosticAnalyzer analyzer, string source)
        {
            var workspace = new AdhocWorkspace();
            var projectId = ProjectId.CreateNewId();
            var versionStamp = VersionStamp.Create();

            var solution = workspace.CurrentSolution
                .AddProject(projectId, "CSCodeAnalyzer", "CSCodeAnalyzerTests", LanguageNames.CSharp)
                .AddMetadataReference(projectId, MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddDocument(DocumentId.CreateNewId(projectId), "TestFile.cs", SourceText.From(source))
                .WithProjectCompilationOptions(projectId, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var project = solution.GetProject(projectId);
            var compilation = project.GetCompilationAsync().Result;
            var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));
            var diagnostics = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result;

            return diagnostics.OrderBy(d => d.Location.SourceSpan.Start).ToArray();
        }
    }
}
