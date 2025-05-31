using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.Identifiers
{
    public class PublicPropertyTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckPublicProperty0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    class testClass
                    {

                    }
                }";

            var analyzer    = new PublicPropertyAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckPublicProperty1()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    class testClass
                    {
                        int PublicProperty { get; set; }
                    }
                }";

            var analyzer    = new PublicPropertyAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
