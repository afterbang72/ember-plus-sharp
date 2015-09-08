﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.ComponentModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>Provides methods to project the items in a collection implementing
    /// <see cref="INotifyCollectionChanged"/> into a <see cref="ReadOnlyObservableCollection{T}"/>.</summary>
    public static class ProjectionHelper
    {
        /// <summary>Returns a collection of items projected from the items in <paramref name="originalItems"/>.
        /// </summary>
        /// <typeparam name="TOriginalCollection">The type of <paramref name="originalItems"/>.</typeparam>
        /// <typeparam name="TOriginal">The type of the items in <paramref name="originalItems"/>.</typeparam>
        /// <typeparam name="TProjected">The type of the items in the returned collection.</typeparam>
        /// <param name="originalItems">The collection of original items to project.</param>
        /// <param name="projectionFunction">Represents the method that converts an object of
        /// <typeparamref name="TOriginal"/> type to <typeparamref name="TProjected"/> type. For a given
        /// <typeparamref name="TOriginal"/> object, this method is guaranteed to be called only ever once.</param>
        /// <exception cref="ArgumentNullException"><paramref name="originalItems"/> and/or
        /// <paramref name="projectionFunction"/> equal <c>null</c>.</exception>
        /// <remarks>
        /// <para>All operations on <paramref name="originalItems"/> are automatically matched by an equivalent
        /// operation on the returned collection such that for each item in the original collection there is always
        /// exactly one item in this collection. Moreover, each projected item is always located at the same index as
        /// its original item.</para>
        /// <para>Note: Due to the nature of the <see cref="INotifyCollectionChanged"/> interface and the guarantee that
        /// the projection function is called exactly once for a given original item, the returned collection
        /// must internally store a dictionary that maps each original item to its corresponding projected
        /// item. The implementation currently only ever adds to this dictionary, so it will always contain all original
        /// and their associated projected items that have ever been added to the original collection.</para></remarks>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposable object is returned to the caller.")]
        public static DisposableReadOnlyObservableCollection<TProjected> Project<TOriginalCollection, TOriginal, TProjected>(
            this TOriginalCollection originalItems, Func<TOriginal, TProjected> projectionFunction)
            where TOriginalCollection : IEnumerable<TOriginal>, IList, INotifyCollectionChanged
        {
            return new DisposableReadOnlyObservableCollection<TProjected>(
                new ProjectionCollection<TOriginalCollection, TOriginal, TProjected>(originalItems, projectionFunction));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private sealed class ProjectionCollection<TOriginalCollection, TOriginal, TProjected> :
            SubscribedObservableCollection<TProjected>
            where TOriginalCollection : IEnumerable<TOriginal>, IList, INotifyCollectionChanged
        {
            private readonly Dictionary<TOriginal, TProjected> projectedDictionary =
                new Dictionary<TOriginal, TProjected>();

            private readonly Func<TOriginal, TProjected> projectionFunction;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            internal ProjectionCollection(
                TOriginalCollection originalItems, Func<TOriginal, TProjected> projectionFunction)
            {
                if (projectionFunction == null)
                {
                    throw new ArgumentNullException("projectionFunction");
                }

                this.projectionFunction = projectionFunction;
                var handler = originalItems.AddChangeHandlers<TOriginalCollection, TOriginal>(
                    this.Insert, this.Remove, this.Clear);
                this.RegisterForRemoval(originalItems, handler);
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            private void Insert(int index, TOriginal original)
            {
                this.Insert(index, this.Project(original));
            }

            private void Remove(int index, TOriginal original)
            {
                this.RemoveAt(index);
            }

            private TProjected Project(TOriginal original)
            {
                TProjected result;

                if (!this.projectedDictionary.TryGetValue(original, out result))
                {
                    result = this.projectionFunction(original);
                    this.projectedDictionary.Add(original, result);
                }

                return result;
            }
        }
    }
}
