using System;
using Xunit;
using SuperAwesome.PriorityQueue.Infrastructure;

namespace SuperAwesome.PriorityQueue.Infrastructure.Tests
{
    public class PriorityQueueTests
    {
        [Fact]
        public void Empty_Queue_Returns_Nothing()
        {
            var q = BuildQueue();
            Assert.Throws<InvalidOperationException>(() => q.Dequeue());
        }

        [Fact]
        public void Queue_With_One_Item_Returns_Item()
        {
            var q = BuildQueue();
            q.Enqueue(1, "value");
            var item = q.Dequeue();
            Assert.Equal("value", item);
            Assert.True(q.IsEmpty);
        }

        [Fact]
        public void Queue_With_Multiple_Items_Returns_Highest_Priority_Item()
        {
            var q = BuildQueue();
            q.Enqueue(4, "4");
            q.Enqueue(7, "7");
            q.Enqueue(0, "0");
            q.Enqueue(3, "3");
            var item = q.Dequeue();
            Assert.Equal("7", item);
            Assert.Equal(3, q.Count);
        }

        private PriorityQueue<int, string> BuildQueue()
        {
            return new PriorityQueue<int, string>(new TestComparer());
        }
    }
}
