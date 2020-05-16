namespace CenteredIntervalTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Interval.IntervalBound;

    public static class CenteredIntervalTreeFactory
    {
        public static ICenteredIntervalTreeNode<TPoint, TValue> Build<TPoint, TValue>(
            List<IntervalValuePair<TPoint, TValue>> intervalValuePairList,
            IComparer<TPoint> comparer)
        {
            if (!intervalValuePairList.Any())
            {
                return new EmptyCenteredIntervalTree<TPoint, TValue>();
            }

            var center = FindCenterBoundPoint(
                intervalList: intervalValuePairList.Select(iv => iv.Interval),
                pointComparer: comparer);

            var leftBranch = new List<IntervalValuePair<TPoint, TValue>>();
            var rightBranch = new List<IntervalValuePair<TPoint, TValue>>();
            var centerBelonged = new List<IntervalValuePair<TPoint, TValue>>();

            foreach (var intervalValuePair in intervalValuePairList)
            {
                var interval = intervalValuePair.Interval;
                if (interval.Contains(point: center, comparer: comparer) ||
                    interval.IsBoundaryPoint(point: center, comparer: comparer))
                {
                    centerBelonged.Add(intervalValuePair);
                }
                else if (interval.LowerBound.CompareToPoint(point: center, comparer: comparer) < 0)
                {
                    leftBranch.Add(intervalValuePair);
                }
                else
                {
                    rightBranch.Add(intervalValuePair);
                }
            }

            var lowerBoundComparer = new LowerBoundComparer<TPoint>(comparer);

            return new CenteredIntervalTreeNode<TPoint, TValue>(
                leftBranch: Build<TPoint, TValue>(
                    intervalValuePairList: leftBranch,
                    comparer: comparer),
                rightBranch: Build<TPoint, TValue>(
                    intervalValuePairList: rightBranch,
                    comparer: comparer),
                centerBelongedRangeValuePairList: centerBelonged.OrderBy(
                        kv => kv.Interval.LowerBound,
                        comparer: lowerBoundComparer)
                    .ToList(),
                center: center,
                comparer: comparer);
        }

        private static TPoint FindCenterBoundPoint<TPoint>(
            IEnumerable<Interval.IInterval<TPoint>> intervalList,
            IComparer<TPoint> pointComparer)
        {
            var points = intervalList.SelectMany(
                    i => i.GetBoundariesPoint())
                .OrderBy(p => p, pointComparer)
                .ToArray();

            return points.Skip(points.Count() / 2)
                .First();
        }
    }
}