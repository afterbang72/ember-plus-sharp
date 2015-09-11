﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model.Test
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection.")]
    internal sealed class MediaSession : FieldNode<MediaSession>
    {
        [Element(Identifier = "name")]
        internal StringParameter Name { get; private set; }

        [Element(Identifier = "id")]
        internal StringParameter Id { get; private set; }

        [Element(Identifier = "state")]
        internal IntegerParameter State { get; private set; }
    }
}