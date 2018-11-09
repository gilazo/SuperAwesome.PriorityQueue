using System.Collections.Generic;

namespace SuperAwesome.PriorityQueue.Infrastructure.Tests
{
    public class TestComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y - x;
        }
    }
}
