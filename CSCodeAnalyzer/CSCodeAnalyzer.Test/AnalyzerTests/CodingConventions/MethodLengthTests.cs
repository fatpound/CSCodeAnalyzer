using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.CodingConventions
{
    public class MethodLengthTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckMethodLength0()
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

            var analyzer    = new MethodLengthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckMethodLength1()
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

            var analyzer    = new MethodLengthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
