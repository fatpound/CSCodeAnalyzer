using Microsoft.CodeAnalysis;
using Xunit;

namespace CSCodeAnalyzer.Analyzers.CodeStructure
{
    public class NestedIfDepthTests
    {
        private static readonly MetadataReference[] References = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        [Fact]
        public void CheckNestedIfDepth_1()
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
                                if (true)
                                {
                                    if (true)
                                    {
                                        
                                    }
                                }
                            }
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_2()
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
                                if (true)
                                {
                                    if (true)
                                    {
                                        if (true);
                                    }
                                }
                            }
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_3()
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
                                if (true)
                                    if (true)
                                    {
                                        
                                    }
                            }
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_4()
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
                                if (true)
                                    if (true)
                                    {
                                        if (true);
                                    }
                            }
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_5()
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
                                if (true)
                                    if (true)
                                    {
                                        if (true);
                                    }
                            }
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_6()
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
                                if (true)
                                    if (true);
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_7()
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
                                if (true)
                                    if (true)
                                        if (true);
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_8()
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
                                else if (true) // starts counting again?
                                    if (true)
                                        if (true);
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(0, diagnostics.Length);
        }

        [Fact]
        public void CheckNestedIfDepth_9()
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
                                else if (true)  // starts counting again?
                                    if (true)
                                        if (true)
                                            if (true);
                        }
                    }
                }";

            var analyzer = new NestedIfDepthAnalyzer();
            var diagnostics = CodeAnalyzer.GetSortedDiagnostics(analyzer, source);

            Assert.Equal(1, diagnostics.Length);
        }
    }
}

