using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.Naming
{
    public class CamelCaseTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckCamelCase0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class TestClass
                    {
                        int Sayi = 0;

                        public void TestMethod()
                        {
                            int Intejer = 0;
                        }
                    }
                }";

            var analyzer = new CamelCaseAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(2, diagnostics.Length);

            // for (int i = 0; i < diagnostics.Length; i++)
            // {
            //     var diagnostic = diagnostics[i];
            //     var span = diagnostic.Location.SourceSpan;
            //     var actualMethodName = source.Substring(span.Start, span.Length);
            //     string expectedMethodName = char.ToLower(actualMethodName[0]) + actualMethodName.Substring(1);
            // 
            //     Assert.Equal(expectedMethodName, actualMethodName);
            // }
        }

        [Fact]
        public void CheckCamelCase1()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod()
                        {

                        }
                    }
                }";

            var analyzer = new CamelCaseAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }
    }
}
