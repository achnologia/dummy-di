using DummyDependencyInjection.Interfaces;

namespace DummyDependencyInjection;

public class ServiceWithMultiplePublicCtorsWithParams : IServiceWithMultiplePublicCtorsWithParams
{
    public ServiceWithMultiplePublicCtorsWithParams(IService service)
    {
        
    }

    public ServiceWithMultiplePublicCtorsWithParams(IServiceWithConstructor serviceWithConstructor)
    {
        
    }
}