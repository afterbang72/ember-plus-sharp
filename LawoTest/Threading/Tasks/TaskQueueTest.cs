////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.Threading.Tasks
{
    using System;
    using System.Threading.Tasks;

    using Lawo.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>Tests the <see cref="TaskQueue"/> class.</summary>
    [TestClass]
    public sealed class TaskQueueTest : TestBase
    {
        /// <summary>Tests the main use cases.</summary>
        [TestCategory("Unattended")]
        [TestMethod]
        public void MainTest()
        {
            AsyncPump.Run(
                async () =>
                {
                    var counter = 0;
                    var queue = new TaskQueue();
                    var task1 = queue.Enqueue(async () => { await Task.Delay(250); ++counter; });
                    Assert.AreEqual(2, await queue.Enqueue(() => Task.FromResult(++counter)));
                    Assert.AreEqual(2, counter);
                });
        }

        /// <summary>Tests the exceptional cases.</summary>
        [TestCategory("Unattended")]
        [TestMethod]
        public void ExceptionTest()
        {
            var queue = new TaskQueue();

            AssertThrow<ArgumentNullException>(
                () => queue.Enqueue((Func<Task>)null),
                () => queue.Enqueue((Func<Task<string>>)null));
        }
    }
}
