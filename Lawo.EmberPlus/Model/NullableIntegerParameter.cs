﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System.Diagnostics.CodeAnalysis;

    using Lawo.EmberPlus.Ember;
    using Lawo.EmberPlus.Glow;

    /// <summary>Represents a nullable integer parameter in the protocol specified in the
    /// <a href="http://ember-plus.googlecode.com/svn/trunk/documentation/Ember+%20Documentation.pdf">Ember+
    /// Specification</a>.</summary>
    /// <remarks>
    /// <para><b>Thread Safety</b>: Any public static members of this type are thread safe. Any instance members are not
    /// guaranteed to be thread safe.</para>
    /// </remarks>
    [SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance", Justification = "Fewer levels of inheritance would lead to more code duplication.")]
    public sealed class NullableIntegerParameter : NullableNumericParameter<NullableIntegerParameter, long>
    {
        internal sealed override long? ReadValue(EmberReader reader, out ParameterType? parameterType)
        {
            parameterType = ParameterType.Integer;
            return reader.AssertAndReadContentsAsInt64();
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Method is not public, CA bug?")]
        internal sealed override void WriteValue(EmberWriter writer, long? value)
        {
            writer.WriteValue(GlowParameterContents.Value.OuterId, value.Value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private NullableIntegerParameter()
        {
        }
    }
}
