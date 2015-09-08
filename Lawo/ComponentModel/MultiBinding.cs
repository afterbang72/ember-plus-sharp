﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Lawo.Reflection;

    /// <summary>Provides methods to create <see cref="MultiBinding{T}"/> instances.</summary>
    /// <remarks>
    /// <para>When a <see cref="MultiBinding{T}"/> instance is created, the value of the target property is
    /// calculated from the values of the source properties. Whenever one of the source properties changes, the value of
    /// the target property is recalculated and set.</para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Somehow the CA dictionary does not seem to work here.")]
    public static class MultiBinding
    {
        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="T">The type of the source and target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<T> Create<T>(
            IProperty<INotifyPropertyChanged, T> s1, IProperty<object, T> target)
        {
            return Create(s1, v => v, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1, Func<TS1, TTarget> toTarget, IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            return new MultiBinding<TTarget>(Pack(s1), () => toTarget(s1.Value), target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            Func<TS1, TS2, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            return new MultiBinding<TTarget>(Pack(s1, s2), () => toTarget(s1.Value, s2.Value), target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            Func<TS1, TS2, TS3, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3), () => toTarget(s1.Value, s2.Value, s3.Value), target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            Func<TS1, TS2, TS3, TS4, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(s1.Value, s2.Value, s3.Value, s4.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TS5">The type of the fifth source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TS5, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            IProperty<INotifyPropertyChanged, TS5> s5,
            Func<TS1, TS2, TS3, TS4, TS5, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(s1.Value, s2.Value, s3.Value, s4.Value, s5.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4, s5), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TS5">The type of the fifth source property.</typeparam>
        /// <typeparam name="TS6">The type of the sixth source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TS5, TS6, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            IProperty<INotifyPropertyChanged, TS5> s5,
            IProperty<INotifyPropertyChanged, TS6> s6,
            Func<TS1, TS2, TS3, TS4, TS5, TS6, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4, s5, s6), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TS5">The type of the fifth source property.</typeparam>
        /// <typeparam name="TS6">The type of the sixth source property.</typeparam>
        /// <typeparam name="TS7">The type of the seventh source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            IProperty<INotifyPropertyChanged, TS5> s5,
            IProperty<INotifyPropertyChanged, TS6> s6,
            IProperty<INotifyPropertyChanged, TS7> s7,
            Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4, s5, s6, s7), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TS5">The type of the fifth source property.</typeparam>
        /// <typeparam name="TS6">The type of the sixth source property.</typeparam>
        /// <typeparam name="TS7">The type of the seventh source property.</typeparam>
        /// <typeparam name="TS8">The type of the eighth source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            IProperty<INotifyPropertyChanged, TS5> s5,
            IProperty<INotifyPropertyChanged, TS6> s6,
            IProperty<INotifyPropertyChanged, TS7> s7,
            IProperty<INotifyPropertyChanged, TS8> s8,
            Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound =
                () => toTarget(s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value, s8.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4, s5, s6, s7, s8), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TS5">The type of the fifth source property.</typeparam>
        /// <typeparam name="TS6">The type of the sixth source property.</typeparam>
        /// <typeparam name="TS7">The type of the seventh source property.</typeparam>
        /// <typeparam name="TS8">The type of the eighth source property.</typeparam>
        /// <typeparam name="TS9">The type of the ninth source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            IProperty<INotifyPropertyChanged, TS5> s5,
            IProperty<INotifyPropertyChanged, TS6> s6,
            IProperty<INotifyPropertyChanged, TS7> s7,
            IProperty<INotifyPropertyChanged, TS8> s8,
            IProperty<INotifyPropertyChanged, TS9> s9,
            Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(
                s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value, s8.Value, s9.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4, s5, s6, s7, s8, s9), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TS1">The type of the first source property.</typeparam>
        /// <typeparam name="TS2">The type of the second source property.</typeparam>
        /// <typeparam name="TS3">The type of the third source property.</typeparam>
        /// <typeparam name="TS4">The type of the fourth source property.</typeparam>
        /// <typeparam name="TS5">The type of the fifth source property.</typeparam>
        /// <typeparam name="TS6">The type of the sixth source property.</typeparam>
        /// <typeparam name="TS7">The type of the seventh source property.</typeparam>
        /// <typeparam name="TS8">The type of the eighth source property.</typeparam>
        /// <typeparam name="TS9">The type of the ninth source property.</typeparam>
        /// <typeparam name="TS10">The type of the tenth source property.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TTarget>(
            IProperty<INotifyPropertyChanged, TS1> s1,
            IProperty<INotifyPropertyChanged, TS2> s2,
            IProperty<INotifyPropertyChanged, TS3> s3,
            IProperty<INotifyPropertyChanged, TS4> s4,
            IProperty<INotifyPropertyChanged, TS5> s5,
            IProperty<INotifyPropertyChanged, TS6> s6,
            IProperty<INotifyPropertyChanged, TS7> s7,
            IProperty<INotifyPropertyChanged, TS8> s8,
            IProperty<INotifyPropertyChanged, TS9> s9,
            IProperty<INotifyPropertyChanged, TS10> s10,
            Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(
                s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value, s8.Value, s9.Value, s10.Value);
            return new MultiBinding<TTarget>(Pack(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10), bound, target);
        }

        /// <summary>Creates and returns a <see cref="MultiBinding{T}"/> instance.</summary>
        /// <typeparam name="TSource">The type of the source properties.</typeparam>
        /// <typeparam name="TTarget">The type of the target property.</typeparam>
        /// <exception cref="ArgumentNullException">At least one of the arguments is equal to <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Longer names would lead to unwieldy code.")]
        public static MultiBinding<TTarget> Create<TSource, TTarget>(
            IEnumerable<IProperty<INotifyPropertyChanged, TSource>> sources,
            Func<IEnumerable<TSource>, TTarget> toTarget,
            IProperty<object, TTarget> target)
        {
            if (sources == null)
            {
                throw new ArgumentNullException("sources");
            }

            AssertNotNull(toTarget);
            Func<TTarget> bound = () => toTarget(sources.Select(s => s.Value));
            return new MultiBinding<TTarget>(sources.ToArray(), bound, target);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void AssertNotNull(Delegate toTarget)
        {
            if (toTarget == null)
            {
                throw new ArgumentNullException("toTarget");
            }
        }

        private static IProperty<INotifyPropertyChanged>[] Pack(params IProperty<INotifyPropertyChanged>[] properties)
        {
            return properties;
        }
    }
}
