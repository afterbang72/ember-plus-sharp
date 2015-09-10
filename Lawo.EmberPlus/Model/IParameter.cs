﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>Represents a parameter in the protocol specified in the
    /// <a href="http://ember-plus.googlecode.com/svn/trunk/documentation/Ember+%20Documentation.pdf">Ember+
    /// Specification</a>.</summary>
    /// <remarks>Members for which the provider does not define a value are equal to <c>null</c>.</remarks>
    public interface IParameter : IElementWithSchemas
    {
        /// <summary>Gets or sets <b>value</b>.</summary>
        /// <exception cref="ArgumentException">Attempted to set a value with a type that does not match
        /// <see cref="Type"/>.</exception>
        /// <exception cref="ArgumentNullException">Attempted to set to <c>null</c>.</exception>
        object Value { get; set; }

        /// <summary>Gets <b>minimum</b>.</summary>
        object Minimum { get; }

        /// <summary>Gets <b>maximum</b>.</summary>
        object Maximum { get; }

        /// <summary>Gets <b>access</b>.</summary>
        ParameterAccess Access { get; }

        /// <summary>Gets <b>format</b>.</summary>
        string Format { get; }

        /// <summary>Gets <b>factor</b>.</summary>
        /// <remarks>If this property is not equal to <c>null</c>, the value of <see cref="Value"/> should be divided by
        /// the value of this property before further processing (e.g. the display on a UI). Vice versa, client code
        /// must multiply with this factor before setting <see cref="Value"/>.</remarks>
        int? Factor { get; }

        /// <summary>Gets <b>formula</b>.</summary>
        /// <remarks>The formulas are separated by <c>\\n</c>. If this property is not equal to <c>null</c>, the value of
        /// <see cref="Value"/> should be converted with the first formula before further processing (e.g. the display
        /// on a UI). Vice versa, client code must use the second formula before setting <see cref="Value"/>.</remarks>
        string Formula { get; }

        /// <summary>Gets <b>defaultValue</b>.</summary>
        object DefaultValue { get; }

        /// <summary>Gets <b>type</b>.</summary>
        ParameterType Type { get; }

        /// <summary>Gets <b>streamIdentifier</b> (not yet supported).</summary>
        int? StreamIdentifier { get; }

        /// <summary>Gets <b>enumMap</b>.</summary>
        /// <remarks>The returned collection contains the entries of either the <b>enumeration</b> field or the
        /// <b>enumMap</b> field.
        /// If the provider sets both fields, the collection contains the entries of the field that appeared last in the
        /// EmBER-encoded data.</remarks>
        IReadOnlyList<KeyValuePair<string, int>> EnumMap { get; }

        /// <summary>Gets <b>streamDescriptor</b>.</summary>
        StreamDescription? StreamDescriptor { get; }
    }
}
