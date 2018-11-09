namespace SuperAwesome.PriorityQueue.Application
{
    public interface IQueue<TPriority, TValue>
    {
        int Count { get; }
        bool IsEmpty { get; }
        void Enqueue(TPriority priority, TValue value);
        TValue Dequeue();
        TValue Peek();
    }
}
