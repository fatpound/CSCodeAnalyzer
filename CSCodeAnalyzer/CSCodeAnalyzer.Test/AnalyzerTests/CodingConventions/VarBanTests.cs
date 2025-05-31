using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    public class VarBanTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckVar0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod()
                        {
                            var i = 0;
                        }
                    }
                }";

            var analyzer    = new VarBanAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
