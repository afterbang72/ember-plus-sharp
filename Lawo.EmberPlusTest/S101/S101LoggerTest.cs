﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.S101
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using Lawo.EmberPlus.Glow;
    using Lawo.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>Tests <see cref="S101Logger"/>.</summary>
    [TestClass]
    public class S101LoggerTest : TestBase
    {
        /// <summary>Tests the main use cases.</summary>
        [TestCategory("Unattended")]
        [TestMethod]
        public void MainTest()
        {
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                AssertThrow<ArgumentNullException>(
                    () => new S101Logger(null, writer).Dispose(),
                    () => new S101Logger(GlowTypes.Instance, (TextWriter)null).Dispose(),
                    () => new S101Logger(GlowTypes.Instance, (XmlWriter)null).Dispose());

                using (var logger = new S101Logger(GlowTypes.Instance, writer))
                {
                    AssertThrow<ArgumentNullException>(
                        () => logger.LogData("Whatever", "Send", null, 0, 0),
                        () => logger.LogMessage(null, new S101Message(0x00, new KeepAliveRequest()), null),
                        () => logger.LogMessage("Send", null, null),
                        () => logger.LogException(null));
                }
            }
        }
    }
}
