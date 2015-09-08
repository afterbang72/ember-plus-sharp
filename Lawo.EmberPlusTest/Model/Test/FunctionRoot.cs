﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model.Test
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection.")]
    internal sealed class FunctionRoot : Root<FunctionRoot>
    {
        internal IFunction Function0 { get; private set; }

        internal Function<long, Result<long>> Function1 { get; private set; }

        internal Function<double, string, Result<double, string>> Function2 { get; private set; }

        internal Function<bool, byte[], long, Result<bool, byte[], long>> Function3 { get; private set; }

        internal FunctionNode Node { get; private set; }
    }
}
