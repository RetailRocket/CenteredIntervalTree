namespace CenteredIntervalTreeTests.ComparerTests
{
    using System;
    using System.Collections.Generic;
    using CenteredIntervalTree;
    using Interval.IntervalBound.LowerBound;
    using Interval.IntervalBound.UpperBound;
    using Xunit;

    public class CharsWithDifferentCasesCanBeCountedAsEqual
    {
        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        public void PointCanBeFoundInBothCases(
            string point)
        {
            var intervalTree = CenteredIntervalTreeFactory
                .Build(
                    intervalValuePairList: new List<IntervalValuePair<string, string>>
                    {
                        new IntervalValuePair<string, string>(
                            interval: new Interval.Interval<string>(
                                lowerBound: new ClosedLowerBound<string>(point),
                                upperBound: new ClosedUpperBound<string>(point)),
                            value: "Value"),
                    },
                    comparer: StringComparer.OrdinalIgnoreCase);

            Assert.Single(
                collection: intervalTree.Query(
                    point: point.ToLower()));

            Assert.Single(
                collection: intervalTree.Query(
                    point: point.ToUpper()));
        }

        [Theory]
        [InlineData("a", "b")]
        [InlineData("b", "c")]
        public void PointDoNotFinds(
            string point,
            string query)
        {
            var intervalTree = CenteredIntervalTreeFactory
                .Build(
                    intervalValuePairList: new List<IntervalValuePair<string, string>>
                    {
                        new IntervalValuePair<string, string>(
                            interval: new Interval.Interval<string>(
                                lowerBound: new ClosedLowerBound<string>(point),
                                upperBound: new ClosedUpperBound<string>(point)),
                            value: "Value"),
                    },
                    comparer: StringComparer.OrdinalIgnoreCase);

            Assert.Empty(
                collection: intervalTree.Query(
                    point: query));

            Assert.Empty(
                collection: intervalTree.Query(
                    point: query));
        }
    }
}