// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Test.Utilities;
using Microsoft.CodeAnalysis.UnitTests;
using Xunit;

namespace Microsoft.ApiDesignGuidelines.Analyzers.UnitTests
{
    public class ExceptionsShouldBePublicFixerTests : CodeFixTestBase
    {
        protected override DiagnosticAnalyzer GetBasicDiagnosticAnalyzer()
        {
            return new BasicExceptionsShouldBePublicAnalyzer();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new CSharpExceptionsShouldBePublicAnalyzer();
        }

        protected override CodeFixProvider GetBasicCodeFixProvider()
        {
            return new BasicExceptionsShouldBePublicFixer();
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CSharpExceptionsShouldBePublicFixer();
        }

        [Fact]
        public void TestCSharpNonPublicException()
        {
            var original = @"
using System;

class InternalException : Exception
{
}";

            var expected = @"
using System;

public class InternalException : Exception
{
}";

            VerifyCSharpFix(original, expected);
        }

        [Fact]
        public void TestCSharpNonPublicException2()
        {
            var original = @"
using System;

private class PrivateException : SystemException
{
}";

            var expected = @"
using System;

public class PrivateException : SystemException
{
}";

            VerifyCSharpFix(original, expected);
        }

        [Fact]
        public void TestVBasicNonPublicException()
        {
            var original = @"
Imports System

Class InternalException
   Inherits Exception
End Class";

            var expected = @"
Imports System

Public Class InternalException
   Inherits Exception
End Class";

            VerifyBasicFix(original, expected);
        }

        [Fact]
        public void TestVBasicNonPublicException2()
        {
            var original = @"
Imports System

Private Class PrivateException
   Inherits SystemException
End Class";

            var expected = @"
Imports System

Public Class PrivateException
   Inherits SystemException
End Class";

            VerifyBasicFix(original, expected);
        }
    }
}