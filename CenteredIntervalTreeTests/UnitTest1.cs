namespace CenteredIntervalTreeTests
{
    using System;
    using System.Collections.Generic;
    using CenteredIntervalTree;
    using Interval;
    using Interval.IntervalBound.LowerBound;
    using Interval.IntervalBound.UpperBound;
    using Xunit;

    public class UnitTest1
    {
        [Fact]
        public void Test()
        {
            var intervalTree = CenteredIntervalTreeFactory
                .Build(
                    intervalValuePairList: new List<IntervalValuePair<int, string>>
                    {
                        new IntervalValuePair<int, string>(
                            interval: (Interval<int>)IntervalFactory.BuildOpenInterval(
                                lowerBoundaryPoint: 0,
                                upperBoundaryPoint: 10,
                                comparer: Comparer<int>.Default),
                            value: "A"),
                        new IntervalValuePair<int, string>(
                            interval: (Interval<int>)IntervalFactory.BuildClosedInterval(
                                lowerBoundaryPoint: 0,
                                upperBoundaryPoint: 10,
                                comparer: Comparer<int>.Default),
                            value: "B"),
                    },
                    comparer: Comparer<int>.Default);

            var resultOnlyB = intervalTree.Query(
                point: 0);

            Assert.Single(resultOnlyB);
            Assert.Contains(
                collection: resultOnlyB,
                iv => iv.Value == "B");

            var resultBoth = intervalTree.Query(
                point: 1);

            Assert.Equal(
                expected: 2,
                actual: resultBoth.Count);

            Assert.Contains(
                collection: resultBoth,
                iv => iv.Value == "B");

            Assert.Contains(
                collection: resultBoth,
                iv => iv.Value == "A");
        }

        [Fact]
        public void ComparersTest()
        {
            var intervalTree = CenteredIntervalTreeFactory
                .Build(
                    intervalValuePairList: new List<IntervalValuePair<string, string>>
                    {
                        new IntervalValuePair<string, string>(
                            interval: (Interval<string>)IntervalFactory.BuildOpenInterval(
                                lowerBoundaryPoint: "a",
                                upperBoundaryPoint: "z",
                                comparer: Comparer<string>.Default),
                            value: "FIRST"),
                        new IntervalValuePair<string, string>(
                            interval: (Interval<string>)IntervalFactory.BuildClosedInterval(
                                lowerBoundaryPoint: "A",
                                upperBoundaryPoint: "Z",
                                comparer: Comparer<string>.Default),
                            value: "SECOND"),
                    },
                    comparer: StringComparer.OrdinalIgnoreCase);

            Assert.Contains(
                collection: intervalTree.Query(
                    point: "y"),
                iv => iv.Value == "FIRST");

            Assert.Contains(
                collection: intervalTree.Query(
                    point: "Y"),
                iv => iv.Value == "SECOND");
        }
    }
}