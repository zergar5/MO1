using MO1.Core;

namespace MO1.Methods;

public class DichotomyMethod : IExtremumSearchMethod
{
    public double FindMinimum(Interval minimumInterval, Func<double, double> function)
    {
        var a = minimumInterval.LeftPoint;
        var b = minimumInterval.RightPoint;

        var x1 = (a + b - MethodsConfig.Delta) / 2;
        var x2 = (a + b + MethodsConfig.Delta) / 2;

        var f1Value = function(x1);
        var f2Value = function(x2);

        if (f1Value < f2Value)
        {
            b = x2;
        }
        else if (f1Value > f2Value)
        {
            a = x1;
        }

        var currentMinimumInterval = new Interval(a, b);

        IterationInformer.Inform(1, currentMinimumInterval);

        var maxIterations = Math.Log((minimumInterval.RightPoint - minimumInterval.LeftPoint) / MethodsConfig.Eps) /
                            Math.Log(2);

        for (var k = 2; Math.Abs(b - a) > MethodsConfig.Eps && k <= maxIterations; k++)
        {
            x1 = (a + b - MethodsConfig.Delta) / 2;
            x2 = (a + b + MethodsConfig.Delta) / 2;

            f1Value = function(x1);
            f2Value = function(x2);

            if (f1Value < f2Value)
            {
                b = x2;
                currentMinimumInterval.RightPoint = b;
            }
            else if (f1Value > f2Value)
            {
                a = x1;
                currentMinimumInterval.LeftPoint = a;
            }

            IterationInformer.Inform(k, currentMinimumInterval);
        }

        return (a + b) / 2;
    }
}