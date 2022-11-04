using DummyDependencyInjection.Interfaces;

namespace DummyDependencyInjection;

public class ServiceWithDefaultValue : IServiceWithDefaultValue
{
    public const int DefaultValue = 10;

    public int? SetValue = null;
    
    public int GetValue() => SetValue ?? DefaultValue;
}