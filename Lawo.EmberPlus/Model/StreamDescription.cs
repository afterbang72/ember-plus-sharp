﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System;

    /// <summary>Describes the format and the offset of a value in a stream.</summary>
    /// <remarks>
    /// <para><b>Thread Safety</b>: Any public static members of this type are thread safe. Any instance members are not
    /// guaranteed to be thread safe.</para>
    /// </remarks>
    public struct StreamDescription : IEquatable<StreamDescription>
    {
        private readonly StreamFormat format;
        private readonly int offset;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets the format.</summary>
        public StreamFormat Format
        {
            get { return this.format; }
        }

        /// <summary>Gets the offset in bytes.</summary>
        public int Offset
        {
            get { return this.offset; }
        }

        /// <summary>See <see cref="IEquatable{T}.Equals"/>.</summary>
        public bool Equals(StreamDescription other)
        {
            return (this.format == other.format) && (this.offset == other.offset);
        }

        /// <summary>See <see cref="object.Equals(object)"/>.</summary>
        public override bool Equals(object obj)
        {
            var other = obj as StreamDescription?;
            return other.HasValue && this.Equals(other.Value);
        }

        /// <summary>See <see cref="object.GetHashCode"/>.</summary>
        public override int GetHashCode()
        {
            return HashCode.Combine((int)this.format, this.offset);
        }

        /// <summary>Determines whether two specified instances of <see cref="StreamDescription"/> are equal.</summary>
        public static bool operator ==(StreamDescription left, StreamDescription right)
        {
            return left.Equals(right);
        }

        /// <summary>Determines whether two specified instances of <see cref="StreamDescription"/> are not equal.
        /// </summary>
        public static bool operator !=(StreamDescription left, StreamDescription right)
        {
            return !left.Equals(right);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        internal StreamDescription(StreamFormat format, int offset)
        {
            this.format = format;
            this.offset = offset;
        }
    }
}