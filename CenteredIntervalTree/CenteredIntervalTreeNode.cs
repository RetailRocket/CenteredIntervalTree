namespace CenteredIntervalTree
{
    using System.Collections.Generic;
    using Operations;

    public class CenteredIntervalTreeNode<TPoint, TValue>
        : ICenteredIntervalTreeNode<TPoint, TValue>
    {
        private readonly IComparer<TPoint> comparer;

        public CenteredIntervalTreeNode(
            ICenteredIntervalTreeNode<TPoint, TValue> leftBranch,
            ICenteredIntervalTreeNode<TPoint, TValue> rightBranch,
            List<IntervalValuePair<TPoint, TValue>> centerBelongedRangeValuePairList,
            TPoint center,
            IComparer<TPoint> comparer)
        {
            this.comparer = comparer;
            this.LeftBranch = leftBranch;
            this.RightBranch = rightBranch;
            this.CenterBelongedRangeValuePairList = centerBelongedRangeValuePairList;
            this.Center = center;
        }

        private ICenteredIntervalTreeNode<TPoint, TValue> LeftBranch { get; }

        private ICenteredIntervalTreeNode<TPoint, TValue> RightBranch { get; }

        private List<IntervalValuePair<TPoint, TValue>> CenterBelongedRangeValuePairList { get; }

        private TPoint Center { get; }

        public List<IntervalValuePair<TPoint, TValue>> Query(
            TPoint point)
        {
            var result = new List<IntervalValuePair<TPoint, TValue>>();

            foreach (var centerBelongedRangeValue in this.CenterBelongedRangeValuePairList)
            {
                var interval = centerBelongedRangeValue.Interval;

                if (interval.LowerBound.CompareToPoint(
                        point: point,
                        comparer: this.comparer) > 0)
                {
                    break;
                }

                if (interval.Contains(
                    point: point,
                    comparer: this.comparer))
                {
                    result.Add(centerBelongedRangeValue);
                }
            }

            var centerCompare = this.comparer.Compare(point, this.Center);
            if (centerCompare < 0)
            {
                result.AddRange(
                    collection: this.LeftBranch
                        .Query(
                            point: point));
            }
            else if (centerCompare > 0)
            {
                result.AddRange(
                    collection: this.RightBranch
                        .Query(
                            point: point));
            }

            return result;
        }
    }
}