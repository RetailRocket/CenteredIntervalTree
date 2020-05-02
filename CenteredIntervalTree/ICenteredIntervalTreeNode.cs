namespace CenteredIntervalTree
{
    using System.Collections.Generic;

    public interface ICenteredIntervalTreeNode<TPoint, TValue>
    {
        List<IntervalValuePair<TPoint, TValue>> Query(
            TPoint point);
    }
}