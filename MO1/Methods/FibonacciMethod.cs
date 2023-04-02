using System.Linq.Expressions;
using MO1.Core;

namespace MO1.Methods;

public class FibonacciMethod : IExtremumSearchMethod
{
    public double FindMinimum(Interval minimumInterval, Func<double, double> function)
    {
        var a = minimumInterval.LeftPoint;
        var b = minimumInterval.RightPoint;

        var i = 1;
        var fibonacciNumber = CalcFibonacciNumber(i);

        for (i = 2; (b - a) / MethodsConfig.Eps > fibonacciNumber; i++)
        {
            fibonacciNumber = CalcFibonacciNumber(i);
        }

        var n = i - 2;

        var x1 = a + CalcFibonacciNumber(n) / CalcFibonacciNumber(n + 2) * (b - a);
        var x2 = a + CalcFibonacciNumber(n + 1) / CalcFibonacciNumber(n + 2) * (b - a);

        var f1Value = function(x1);
        var f2Value = function(x2);

        if (f1Value < f2Value)
        {
            b = x2;
            x2 = x1;
            f2Value = f1Value;
            x1 = a + CalcFibonacciNumber(n) / CalcFibonacciNumber(n + 2) * (b - a);
            f1Value = function(x1);
        }
        else if (f1Value > f2Value)
        {
            a = x1;
            x1 = x2;
            f1Value = f2Value;
            x2 = a + CalcFibonacciNumber(n + 1) / CalcFibonacciNumber(n + 2) * (b - a);
            f2Value = function(x2);
        }

        var currentMinimumInterval = new Interval(a, b);

        IterationInformer.Inform(1, currentMinimumInterval);

        var maxIterations = Math.Log((minimumInterval.RightPoint - minimumInterval.LeftPoint) / MethodsConfig.Eps) /
                            Math.Log((Math.Sqrt(5) + 1) / 2);

        for (var k = 2; k != n; k++)
        {
            if (f1Value < f2Value)
            {
                b = x2;
                x2 = x1;
                f2Value = f1Value;
                x1 = a + CalcFibonacciNumber(n - k + 1) / CalcFibonacciNumber(n - k + 3) * (b - a);
                f1Value = function(x1);
                currentMinimumInterval.RightPoint = b;
            }
            else if (f1Value > f2Value)
            {
                a = x1;
                x1 = x2;
                f1Value = f2Value;
                x2 = a + CalcFibonacciNumber(n - k + 2) / CalcFibonacciNumber(n - k + 3) * (b - a);
                f2Value = function(x2);
                currentMinimumInterval.LeftPoint = a;
            }

            IterationInformer.Inform(k, currentMinimumInterval);
        }

        return (a + b) / 2;
    }

    private double CalcFibonacciNumber(int n)
    {
        return (Math.Pow((1 + Math.Sqrt(5)) / 2, n) - Math.Pow((1 - Math.Sqrt(5)) / 2, n)) / Math.Sqrt(5);
    }
}