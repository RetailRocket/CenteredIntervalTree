namespace CenteredIntervalTree
{
    using Interval;

    public class IntervalValuePair<TPoint, TValue>
    {
        public IntervalValuePair(
            Interval<TPoint> interval,
            TValue value)
        {
            this.Interval = interval;
            this.Value = value;
        }

        public Interval<TPoint> Interval { get; }

        public TValue Value { get; }
    }
}