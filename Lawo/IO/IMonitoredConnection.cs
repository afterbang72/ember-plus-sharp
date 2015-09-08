﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.IO
{
    using System;

    /// <summary>Represents a connection between two entities that is monitored for connection loss.</summary>
    public interface IMonitoredConnection : IDisposable
    {
        /// <summary>Occurs when the connection has been lost.</summary>
        /// <remarks>
        /// <para>This event is raised in the following situations:
        /// <list type="bullet">
        /// <item>There was a communication error, or</item>
        /// <item>The remote entity has failed to answer a request within a given timeout, or</item>
        /// <item>The remote entity has gracefully shutdown its connection, or</item>
        /// <item>Client code has called <see cref="IDisposable.Dispose"/>.</item>
        /// </list>
        /// For the first two cases <see cref="ConnectionLostEventArgs.Exception"/> indicates the source of the error.
        /// For the the last two cases <see cref="ConnectionLostEventArgs.Exception"/> is <c>null</c>.</para>
        /// </remarks>
        event EventHandler<ConnectionLostEventArgs> ConnectionLost;
    }
}
