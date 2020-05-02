namespace CenteredIntervalTree
{
    using System.Collections.Generic;

    public class EmptyCenteredIntervalTreeFactory<TPoint, TValue>
        : ICenteredIntervalTreeNode<TPoint, TValue>
    {
        public List<IntervalValuePair<TPoint, TValue>> Query(
            TPoint point)
        {
            return new List<IntervalValuePair<TPoint, TValue>>();
        }
    }
}