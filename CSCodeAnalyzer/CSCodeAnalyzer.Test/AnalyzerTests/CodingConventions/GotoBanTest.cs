using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    public class GotoBanTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckGoto0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod()
                        {
                            label:
                                    goto label;
                        }
                    }
                }";

            var analyzer = new GotoBanAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
