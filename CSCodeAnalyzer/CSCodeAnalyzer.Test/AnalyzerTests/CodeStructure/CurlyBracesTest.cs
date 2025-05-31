using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.CodeStructure
{
    public class CurlyBracesTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckCurlyBraces0()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod(int x, int y, int z)
                        {
                            if (true)
                            {

                            }
                        }
                    }
                }";

            var analyzer = new CurlyBracesAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckCurlyBraces1()
        {
            string source = @"
                using System;

                namespace TestNamespace
                {
                    public class testClass
                    {
                        public void testMethod(int x, int y, int z)
                        {
                            if (true){
                                int i = 0;
                                int i = 0;
                            }
                            if (true)
                                int i = 0;
                            if (true)
                                if(true)
                                    int i = 0;
                        }
                    }
                }";

            var analyzer = new CurlyBracesAnalyzer();
            var diagnostics = CSCodeAnalyzer.CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(3, diagnostics.Length);
        }
    }
}
