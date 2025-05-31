using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.CodeStructure
{
    public class ParameterCountTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckParameterCount0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod(int x, int y, int z)
                        {

                        }
                    }
                }";

            var analyzer    = new ParameterCountAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckParameterCount1()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod(int x, int y, int z, int u, int v)
                        {

                        }
                    }
                }";

            var analyzer    = new ParameterCountAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckParameterCount2()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod(int x, int y, int z, int u, int v, int w)
                        {

                        }
                    }
                }";

            var analyzer    = new ParameterCountAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}
