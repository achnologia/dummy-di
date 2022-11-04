using DummyDependencyInjection.Interfaces;

namespace DummyDependencyInjection;

public class ServiceWithConstructor : IServiceWithConstructor
{
    public ServiceWithConstructor(IService serviceOne, IService serviceTwo, IService serviceThree)
    {
        
    }
}