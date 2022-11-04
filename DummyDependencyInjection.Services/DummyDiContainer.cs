namespace DummyDependencyInjection.Services;

public class DummyDiContainer
{
    private readonly Dictionary<Type, TypeDecorator> _registrations = new();

    public DummyDiResolver BuildResolver() => new(this);
        
    public void Add<TInterface, TImplementation>()
        where TImplementation : class, TInterface
    {
        AddInternal(typeof(TInterface), typeof(TImplementation), false);
    }

    public void Add<TInterface>(Func<DummyDiResolver, TInterface> factory)
        where TInterface : class
    {
        AddInternal(typeof(TInterface), typeof(TInterface), false, factory: factory);
    }
    
    public void Add<TInterface>(Type instance)
        where TInterface : class
    {
        AddInternal(typeof(TInterface), instance, false);
    }
    
    public void AddSingleton<TInterface, TImplementation>()
        where TImplementation : class, TInterface
    {
        AddInternal(typeof(TInterface), typeof(TImplementation), true);
    }
    
    public void AddSingleton<TInterface>(Func<DummyDiResolver, TInterface> factory)
        where TInterface : class
    {
        AddInternal(typeof(TInterface), typeof(TInterface), true, factory: factory);
    }

    public void AddSingleton<TInterface>(TInterface instance)
        where TInterface : class
    {
        AddInternal(typeof(TInterface), typeof(TInterface), true, implementation: instance);
    }
    
    private void AddInternal(Type interfaceType, Type implementationType, bool isSingleton, object? implementation = null, Func<DummyDiResolver, object>? factory = null)
    {
        var imp = new TypeDecorator
        {
            IsSingleton = isSingleton,
            Type = implementationType,
            Implementation = implementation,
            Factory = factory
        };

        _registrations.Add(interfaceType, imp);
    }
    
    internal TypeDecorator Get<TInterface>()
    {
        if(!_registrations.TryGetValue(typeof(TInterface), out var implementation))
            throw new Exception();

        return implementation;
    }
    
    internal TypeDecorator Get(Type type)
    {
        if(!_registrations.TryGetValue(type, out var implementation))
            throw new Exception();

        return implementation;
    }
}