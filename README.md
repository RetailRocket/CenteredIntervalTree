![.NET Core](https://github.com/RetailRocket/CenteredIntervalTree/workflows/.NET%20Core/badge.svg)

# Centered Interval Tree

Inpired by https://github.com/mbuchetics/RangeTree, but I've add ability to use open, closed and infinity boundaries of interval.

From [Wikipedia](http://en.wikipedia.org/wiki/Interval_tree):
> In computer science, an interval tree is an ordered tree data structure to hold intervals. Specifically, it allows one to efficiently find all intervals that overlap with any given interval or point. It is often used for windowing queries, for instance, to find all roads on a computerized map inside a rectangular viewport, or to find all visible elements inside a three-dimensional scene.

Based on [Intravl Library](https://github.com/RetailRocket/Interval) and [Interval Operations Library](https://github.com/RetailRocket/Interval.Operations)

## Usage ###

You can build tree by list of interval:

```csharp

var intervalTree = CenteredIntervalTreeFactory
    .Buid(
        intervalValuePairList: new List<IntervalValuePair<int, string>>
        {
            new IntervalValuePair<int, string>(
                interval: new Interval.Interval<int>(
                    lowerBound: new ClosedLowerBound<int>(0),
                    upperBound: new ClosedUpperBound<int>(10)),
                value: "ValueA"),
            new IntervalValuePair<int, string>(
                interval: new Interval.Interval<int>(
                    lowerBound: new OpenLowerBound<int>(5),
                    upperBound: new OpenUpperBound<int>(15)),
                value: "ValueB"),
            new IntervalValuePair<int, string>(
                interval: new Interval.Interval<int>(
                    lowerBound: new InfinityLowerBound<int>(),
                    upperBound: new InfinityUpperBound<int>()),
                value: "ValueC"),
        },
        comparer: Comparer<int>.Default);

```

After that you can find all interval which is overlap the point

```csharp
intervalTree.Query(
  point: 7) // -> List of three IntervalValuePair
```
