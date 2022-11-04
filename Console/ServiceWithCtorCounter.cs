using DummyDependencyInjection.Interfaces;

namespace DummyDependencyInjection;

public class ServiceWithCtorCounter : IServiceWithCtorCounter
{
    private static int _counter = 0;

    public ServiceWithCtorCounter()
    {
        ++_counter;
    }

    public int GetCounter() => _counter;

    public static void Reset() => _counter = 0;
}