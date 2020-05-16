namespace CenteredIntervalTree
{
    using System.Collections.Generic;
    using Interval;
    using Interval.IntervalBound.LowerBound;
    using Interval.IntervalBound.UpperBound;

    public static class IntervalHasBoundaryPointOperation
    {
        public static bool IsBoundaryPoint<TPoint>(
            this Interval<TPoint> interval,
            TPoint point,
            IComparer<TPoint> comparer)
        {
            if (interval.LowerBound is ILowerPointedBound<TPoint> pointedLowerBound &&
                comparer.Compare(pointedLowerBound.Point, point) == 0)
            {
                return true;
            }

            return interval.UpperBound is IUpperPointedBound<TPoint> pointedUpperBound &&
                   comparer.Compare(pointedUpperBound.Point, point) == 0;
        }

        public static bool IsBoundaryPoint<TPoint>(
            this IInterval<TPoint> interval,
            TPoint point,
            IComparer<TPoint> comparer) => interval switch
        {
            Interval<TPoint> notEmptyInterval => notEmptyInterval.IsBoundaryPoint(point, comparer),
            _ => false
        };
    }
}