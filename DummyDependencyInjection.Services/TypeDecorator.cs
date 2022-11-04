namespace DummyDependencyInjection.Services;

public class TypeDecorator
{
    public bool IsSingleton { get; set; }
    public Type Type { get; set; }
    public object? Implementation { get; set; }
    public Func<DummyDiResolver, object>? Factory { get; set; }
}