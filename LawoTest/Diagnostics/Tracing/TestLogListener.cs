﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.Diagnostics.Tracing
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.Tracing;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class TestLogListener : LogListener
    {
        internal class LogEvent
        {
            internal EventLevel EventLevel { get; set; }

            internal int EventId { get; set; }

            internal string LogMessage { get; set; }

            internal uint ThreadId { get; set; }

            internal string FilePath { get; set; }

            internal int LineNumber { get; set; }

            internal string ModluleName { get; set; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public TestLogListener(EventLevel eventLevel)
            : base(eventLevel)
        {
            this.LogEvents = new ObservableCollection<LogEvent>();
        }

        public ObservableCollection<LogEvent> LogEvents { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData != null)
            {
                var logEvent = new LogEvent();

                logEvent.EventLevel = eventData.Level;
                logEvent.EventId = eventData.EventId;

                var payload = eventData.Payload;

                logEvent.LogMessage = payload[0] as string;
                logEvent.ThreadId = (uint)payload[1];
                logEvent.FilePath = payload[2] as string;
                logEvent.LineNumber = (int)payload[3];
                logEvent.ModluleName = payload[4] as string;

                this.LogEvents.Add(logEvent);
            }
        }
    }
}
