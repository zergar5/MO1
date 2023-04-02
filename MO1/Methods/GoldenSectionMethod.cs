using MO1.Core;

namespace MO1.Methods;

public class GoldenSectionMethod : IExtremumSearchMethod
{
    public double FindMinimum(Interval minimumInterval, Func<double, double> function)
    {
        var a = minimumInterval.LeftPoint;
        var b = minimumInterval.RightPoint;

        var x1 = a + 0.381966011d * (b - a);
        var x2 = b - 0.381966011d * (b - a);

        var f1Value = function(x1);
        var f2Value = function(x2);

        if (f1Value < f2Value)
        {
            b = x2;
            x2 = x1;
            f2Value = f1Value;
            x1 = a + 0.381966011d * (b - a);
            f1Value = function(x1);
        }
        else if (f1Value > f2Value)
        {
            a = x1;
            x1 = x2;
            f1Value = f2Value;
            x2 = b - 0.381966011d * (b - a);
            f2Value = function(x2);
        }

        var currentMinimumInterval = new Interval(a, b);

        IterationInformer.Inform(1, currentMinimumInterval);

        var maxIterations = Math.Log((minimumInterval.RightPoint - minimumInterval.LeftPoint) / MethodsConfig.Eps) /
                            Math.Log((Math.Sqrt(5) + 1) / 2);

        for (var k = 2; Math.Abs(b - a) > MethodsConfig.Eps && k <= maxIterations; k++)
        {
            if (f1Value < f2Value)
            {
                b = x2;
                x2 = x1;
                f2Value = f1Value;
                x1 = a + 0.381966011d * (b - a);
                f1Value = function(x1);
                currentMinimumInterval.RightPoint = b;
            }
            else if (f1Value > f2Value)
            {
                a = x1;
                x1 = x2;
                f1Value = f2Value;
                x2 = b - 0.381966011d * (b - a);
                f2Value = function(x2);
                currentMinimumInterval.LeftPoint = a;
            }

            IterationInformer.Inform(k, currentMinimumInterval);
        }

        return (a + b) / 2;
    }
}