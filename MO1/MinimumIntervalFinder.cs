﻿using MO1.Core;

namespace MO1;

public class MinimumIntervalFinder
{
    public Interval FindMinimumInterval(double startPoint, Func<double, double> function)
    {
        var xPrev = startPoint;
        var xNext = 0d;
        var h = 0d;

        var fValue = function(xPrev);

        if (fValue > function(xPrev + MethodsConfig.MinimumIntervalDelta))
        {
            xNext = xPrev + MethodsConfig.MinimumIntervalDelta;
            h = MethodsConfig.MinimumIntervalDelta;
        }
        else if (fValue > function(xPrev - MethodsConfig.MinimumIntervalDelta))
        {
            xNext = xPrev - MethodsConfig.MinimumIntervalDelta;
            h = -MethodsConfig.MinimumIntervalDelta;
        }

        xPrev = xNext;
        h *= 2;
        xNext = xPrev + h;

        var minimumInterval = new Interval(xPrev, xNext);

        IterationInformer.Inform(1, minimumInterval);

        for (var i = 2; function(xPrev) > function(xNext); i++)
        {
            xPrev = xNext;
            h *= 2;
            xNext = xPrev + h;

            minimumInterval.LeftPoint = xPrev;
            minimumInterval.RightPoint = xNext;

            IterationInformer.Inform(i, minimumInterval);
        }

        return minimumInterval;
    }
}