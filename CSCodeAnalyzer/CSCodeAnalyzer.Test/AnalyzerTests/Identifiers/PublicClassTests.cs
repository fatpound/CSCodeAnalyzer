using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.Identifiers
{
    public class PublicClassTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckPublicClass0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    class testClass
                    {

                    }
                }";

            var analyzer    = new PublicClassAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
