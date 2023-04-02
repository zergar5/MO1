using MO1;
using MO1.Core;
using System.Globalization;
using MO1.Methods;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

var function = new Func<double, double>(x => Math.Pow(x - 15d, 2d) + 5d);

var minimumIntervalFinder = new MinimumIntervalFinder();

var minimumInterval = minimumIntervalFinder.FindMinimumInterval(2, function);

Console.WriteLine($"{minimumInterval.LeftPoint} {minimumInterval.RightPoint}");

minimumInterval.RightPoint = 200;
minimumInterval.LeftPoint = 2;

var dichotomyMethod = new DichotomyMethod();

double minimum;

minimum = dichotomyMethod.FindMinimum(minimumInterval, function);

Console.WriteLine(minimum);

var goldenSectionMethod = new GoldenSectionMethod();

minimum = goldenSectionMethod.FindMinimum(minimumInterval, function);

Console.WriteLine(minimum);

var fibonacciMethod = new FibonacciMethod();

minimum = fibonacciMethod.FindMinimum(minimumInterval, function);

Console.Write(minimum);