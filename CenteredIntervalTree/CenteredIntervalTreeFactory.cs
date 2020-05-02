namespace CenteredIntervalTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Operations;
    using Operations.Comparers;

    public static class CenteredIntervalTreeFactory
    {
        public static ICenteredIntervalTreeNode<TPoint, TValue> Buid<TPoint, TValue>(
            List<IntervalValuePair<TPoint, TValue>> intervalValuePairList,
            IComparer<TPoint> comparer)
        {
            if (!intervalValuePairList.Any())
            {
                return new EmptyCenteredIntervalTreeFactory<TPoint, TValue>();
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
                    interval.HasBoundaryPont(point: center, comparer: comparer))
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

            var intervalComparer = new IntervalComparer<TPoint>(
                comparer: comparer);

            return new CenteredIntervalTreeNode<TPoint, TValue>(
                leftBranch: Buid<TPoint, TValue>(
                    intervalValuePairList: leftBranch,
                    comparer: comparer),
                rightBranch: Buid<TPoint, TValue>(
                    intervalValuePairList: rightBranch,
                    comparer: comparer),
                centerBelongedRangeValuePairList: centerBelonged.OrderBy(
                        kv => kv.Interval,
                        comparer: intervalComparer)
                    .ToList(),
                center: center,
                comparer: comparer);
        }

        private static TPoint FindCenterBoundPoint<TPoint>(
            IEnumerable<Interval.Interval<TPoint>> intervalList,
            IComparer<TPoint> pointComparer)
        {
            var points = intervalList.SelectMany(
                    i => i.GetBoundPoints())
                .OrderBy(p => p, pointComparer)
                .ToArray();

            return points.Skip(points.Count() / 2)
                .First();
        }
    }
}