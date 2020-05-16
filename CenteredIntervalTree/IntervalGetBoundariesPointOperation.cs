namespace CenteredIntervalTree
{
    using System.Collections.Generic;
    using Interval;
    using Interval.IntervalBound.LowerBound;
    using Interval.IntervalBound.UpperBound;

    public static class IntervalGetBoundariesPointOperation
    {
        public static List<TPoint> GetBoundariesPoint<TPoint>(
            this Interval<TPoint> interval)
        {
            var result = new List<TPoint>();

            if (interval.LowerBound is ILowerPointedBound<TPoint> pointedLowerBound)
            {
                result.Add(pointedLowerBound.Point);
            }

            if (interval.UpperBound is IUpperPointedBound<TPoint> pointedUpperBound)
            {
                result.Add(pointedUpperBound.Point);
            }

            return result;
        }

        public static List<TPoint> GetBoundariesPoint<TPoint>(
            this IInterval<TPoint> interval) => interval switch
        {
            Interval<TPoint> notEmptyInterval => notEmptyInterval.GetBoundariesPoint(),
            _ => new List<TPoint>()
        };
    }
}