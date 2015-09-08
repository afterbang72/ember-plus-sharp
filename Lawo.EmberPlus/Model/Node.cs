﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    using Lawo.EmberPlus.Ember;
    using Lawo.EmberPlus.Glow;

    /// <summary>Represents a node in the protocol specified in the
    /// <a href="http://ember-plus.googlecode.com/svn/trunk/documentation/Ember+%20Documentation.pdf">Ember+
    /// Specification</a>.</summary>
    /// <typeparam name="TMostDerived">The most-derived subtype of this class.</typeparam>
    /// <remarks>
    /// <para><b>Thread Safety</b>: Any public static members of this type are thread safe. Any instance members are not
    /// guaranteed to be thread safe.</para>
    /// </remarks>
    public abstract class Node<TMostDerived> : NodeBase<TMostDerived>, INode
        where TMostDerived : Node<TMostDerived>
    {
        private readonly ObservableCollection<IElement> observableChildren = new ObservableCollection<IElement>();
        private readonly ReadOnlyObservableCollection<IElement> readOnlyObservableChildren;
        private bool isRoot;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets a value indicating whether this is a root node, see <see cref="INode.IsRoot"/>.</summary>
        public bool IsRoot
        {
            get { return this.GetIsRoot(); }
            private set { this.SetValue(ref this.isRoot, value); }
        }

        /// <summary>See <see cref="INode.GetElement"/>.</summary>
        public IElement GetElement(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            return this.GetElement(path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries), 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Required property is provided by subclasses.")]
        ReadOnlyObservableCollection<IElement> INode.Children
        {
            get { return this.readOnlyObservableChildren; }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Required property is provided by subclasses.")]
        IElement INode.this[int number]
        {
            get { return this.GetChild(number); }
        }

        internal Node()
        {
            this.readOnlyObservableChildren = new ReadOnlyObservableCollection<IElement>(this.observableChildren);
        }

        internal virtual bool GetIsRoot()
        {
            return this.isRoot;
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Method is not public, CA bug?")]
        internal override bool ChangeOnlineStatus(IElement child)
        {
            if (child.IsOnline)
            {
                this.observableChildren.Add(child);
            }
            else
            {
                this.observableChildren.Remove(child);
            }

            return base.ChangeOnlineStatus(child);
        }

        internal sealed override ChildrenState ReadContents(EmberReader reader, ElementType actualType)
        {
            this.AssertElementType(ElementType.Node, actualType);

            while (reader.Read() && (reader.InnerNumber != InnerNumber.EndContainer))
            {
                switch (reader.GetContextSpecificOuterNumber())
                {
                    case GlowNodeContents.Description.OuterNumber:
                        this.Description = reader.AssertAndReadContentsAsString();
                        break;
                    case GlowNodeContents.IsRoot.OuterNumber:
                        this.IsRoot = reader.AssertAndReadContentsAsBoolean();
                        break;
                    case GlowNodeContents.IsOnline.OuterNumber:
                        this.IsOnline = reader.AssertAndReadContentsAsBoolean();
                        var newChildrenState = this.ChildrenState & ChildrenState.Complete;
                        this.SetChildrenState(false, ref newChildrenState);
                        break;
                    case GlowNodeContents.SchemaIdentifiers.OuterNumber:
                        this.ReadSchemaIdentifiers(reader);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return this.ChildrenState;
        }

        internal override void WriteChanges(EmberWriter writer, IInvocationCollection invocationCollection)
        {
            if (this.HasChanges)
            {
                this.WriteChangesCollection(writer, invocationCollection);
                this.HasChanges = false;
            }
        }

        internal sealed override IElement GetElement(string[] pathElements, int index)
        {
            var candidate = base.GetElement(pathElements, index);

            if (candidate != null)
            {
                return candidate;
            }

            var child = (Element)this.observableChildren.FirstOrDefault(c => c.Identifier == pathElements[index]);
            return child == null ? child : child.GetElement(pathElements, index + 1);
        }
    }
}
