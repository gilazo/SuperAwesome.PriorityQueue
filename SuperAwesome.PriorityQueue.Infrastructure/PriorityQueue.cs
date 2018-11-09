using System;
using System.Collections.Generic;
using SuperAwesome.PriorityQueue.Application;

namespace SuperAwesome.PriorityQueue.Infrastructure
{
    public sealed class PriorityQueue<TPriority, TValue> : IQueue<TPriority, TValue>
    {
        private readonly List<KeyValuePair<TPriority, TValue>> _heap;
        private readonly IComparer<TPriority> _comparer;

        private int _lastItemIndex => _heap.Count - 1;

        public PriorityQueue()
            : this(Comparer<TPriority>.Default) { }

        public PriorityQueue(IComparer<TPriority> comparer)
            : this(new List<KeyValuePair<TPriority, TValue>>(), comparer) { }

        public PriorityQueue(List<KeyValuePair<TPriority, TValue>> heap, IComparer<TPriority> comparer)
        {
            if (heap == null) throw new ArgumentNullException("heap", "cannot be null");
            if (comparer == null) throw new ArgumentNullException("comparer", "cannot be null");

            _heap = heap;
            _comparer = comparer;
        }

        public int Count => _heap.Count;
        public bool IsEmpty => Count <= 0;

        public void Enqueue(TPriority priority, TValue value)
        {
            Push(priority, value);
        }

        public TValue Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("Operation not allowed. Queue is empty.");

            Pop(out TValue item);
            return item;
        }

        public TValue Peek()
        {
            if (IsEmpty)
                return default(TValue);

            return _heap[0].Value;
        }

        private void Push(TPriority priority, TValue value)
        {
            _heap.Add(new KeyValuePair<TPriority, TValue>(priority, value));
            SortHeap(_lastItemIndex);
        }

        private void Pop(out TValue item)
        {
            if (IsEmpty)
            {
                _heap.Clear();
                item = default(TValue);
                return;
            }

            item = _heap[0].Value;

            _heap[0] = _heap[_lastItemIndex];
            _heap.RemoveAt(_lastItemIndex);
            SortHeap();
        }

        private void SortHeap(int lowerItem = 0)
        {
            while (lowerItem > 0)
            {
                var higherItem = (lowerItem - 1) / 2;
                if (_comparer.Compare(_heap[higherItem].Key, _heap[lowerItem].Key) > 0)
                {
                    SwapItems(higherItem, lowerItem);
                    lowerItem = higherItem;
                }
                else break;
            }
        }

        private void SwapItems(int item, int otherItem)
        {
            var temp = _heap[item];
            _heap[item] = _heap[otherItem];
            _heap[otherItem] = temp;
        }
    }
}
