using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.Identifiers
{
    public class PrivateMethodTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckPrivateMethod0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class TestClass
                    {
                        private void TestMethod()
                        {

                        }
                    }
                }";

            var analyzer = new PrivateMethodAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }
        [Fact]
        public void CheckPrivateMethod1()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class TestClass
                    {
                        public void TestMethod()
                        {

                        }
                    }
                }";

            var analyzer = new PrivateMethodAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
