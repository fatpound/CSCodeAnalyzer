using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.Naming
{
    public class ConstantRuleTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckConstantRule0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class TestClass
                        {
                            private const int MAX_VALUE = 100;
                        }
                }";

            var analyzer = new ConstantAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }

        [Fact]
        public void CheckConstantRule1()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class TestClass
                    {
                        public void TestMethod()
                        {
                            int i = 0;
                            if (i == 0)
                            {
                                private const int maxValue = 100;
                            }
                        }
                    }
                }";

            var analyzer = new ConstantAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
