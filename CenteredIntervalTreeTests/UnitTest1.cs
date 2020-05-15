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
                            interval: new Interval<int>(
                                lowerBound: new OpenLowerBound<int>(0),
                                upperBound: new OpenUpperBound<int>(10)),
                            value: "A"),
                        new IntervalValuePair<int, string>(
                            interval: new Interval<int>(
                                lowerBound: new ClosedLowerBound<int>(0),
                                upperBound: new ClosedUpperBound<int>(10)),
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
                            interval: new Interval<string>(
                                lowerBound: new OpenLowerBound<string>("a"),
                                upperBound: new OpenUpperBound<string>("z")),
                            value: "FIRST"),
                        new IntervalValuePair<string, string>(
                            interval: new Interval<string>(
                                lowerBound: new ClosedLowerBound<string>("A"),
                                upperBound: new ClosedUpperBound<string>("Z")),
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