namespace DummyDependencyInjection.Services;

public class DummyDiResolver
{
    private readonly DummyDiContainer _container;

    internal DummyDiResolver(DummyDiContainer container)
    {
        _container = container;
    }

    public TImplementation Get<TImplementation>()
    {
        var type = _container.Get<TImplementation>();

        var instance = CreateInstance(type);

        return (TImplementation) instance;
    }
    
    private object CreateInstance(TypeDecorator typeToCreate)
    {
        if (typeToCreate is {IsSingleton: true, Implementation: { }})
            return typeToCreate.Implementation;

        if (typeToCreate.Factory is not null)
        {
            var i = typeToCreate.Factory.Invoke(this);
            
            if (typeToCreate.IsSingleton)
                typeToCreate.Implementation = i;
            
            return i;
        }
        
        var constructors = typeToCreate.Type.GetConstructors();

        if (constructors.Where(x => x.IsPublic).All(x => x.GetParameters().Length == 0))
        {
            var i = Activator.CreateInstance(typeToCreate.Type);
            
            if (typeToCreate.IsSingleton)
                typeToCreate.Implementation = i;
            
            return i;
        }

        var publicConstructorsWithParams = constructors.Where(x => x.IsPublic && x.GetParameters().Length > 0).ToList();
        
        if(publicConstructorsWithParams.Count > 1)
            throw new Exception();

        var publicCtor = publicConstructorsWithParams.FirstOrDefault();
        var ctorParams = publicCtor.GetParameters();

        var paramInstances = new List<object>();
        
        foreach (var p in ctorParams)
        {
            var i = CreateInstance(_container.Get(p.ParameterType));

            paramInstances.Add(i);
        }

        var instance = Activator.CreateInstance(typeToCreate.Type, paramInstances.ToArray());
        
        if (typeToCreate.IsSingleton)
            typeToCreate.Implementation = instance;

        return instance;
    }
}