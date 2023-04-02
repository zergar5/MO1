using MO1.Core;

namespace MO1;

public class IterationInformer
{
    public static void Inform(int iteration, Interval interval)
    {
        Console.WriteLine($"{iteration} {interval.LeftPoint} {interval.RightPoint}");
    }
}