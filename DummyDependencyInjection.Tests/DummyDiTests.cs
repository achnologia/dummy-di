using System;
using DummyDependencyInjection.Interfaces;
using DummyDependencyInjection.Services;
using Xunit;

namespace DummyDependencyInjection.Tests;

public class DummyDiTests
{
    [Fact]
    public void Should_Resolve()
    {
        // Arrange
        var container = new DummyDiContainer();
        container.Add<IService, Service>();
        
        var di = container.BuildResolver();

        // Act
        var service = di.Get<IService>();

        //Assert
        Assert.NotNull(service);
    }
    
    [Fact]
    public void Should_ResolveWithConstructor()
    {
        // Arrange
        var container = new DummyDiContainer();
        container.Add<IService, Service>();
        container.Add<IServiceWithConstructor, ServiceWithConstructor>();
        
        var di = container.BuildResolver();

        // Act
        var service = di.Get<IServiceWithConstructor>();

        //Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Should_ResolveEverytime()
    {
        // Arrange
        ServiceWithCtorCounter.Reset();
        var container = new DummyDiContainer();
        container.Add<IServiceWithCtorCounter, ServiceWithCtorCounter>();
        
        var di = container.BuildResolver();

        // Act
        var service = (ServiceWithCtorCounter)di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();

        //Assert
        Assert.NotNull(service);
        Assert.Equal(3, service.GetCounter());
    }
    
    [Fact]
    public void Should_ResolveSingleton()
    {
        // Arrange
        ServiceWithCtorCounter.Reset();
        var container = new DummyDiContainer();
        container.AddSingleton<IServiceWithCtorCounter, ServiceWithCtorCounter>();
        
        var di = container.BuildResolver();

        // Act
        var service = (ServiceWithCtorCounter)di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();

        //Assert
        Assert.NotNull(service);
        Assert.Equal(1, service.GetCounter());
    }
    
    [Fact]
    public void Should_ThrowServiceWasNotRegistered()
    {
        // Arrange
        var container = new DummyDiContainer();

        var di = container.BuildResolver();

        // Act
        

        //Assert
        Assert.Throws<Exception>(() => di.Get<IService>());
    }
    
    [Fact]
    public void Should_ThrowServiceHasMultiplePublicCtorsWithParams()
    {
        // Arrange
        var container = new DummyDiContainer();
        container.Add<IServiceWithMultiplePublicCtorsWithParams, ServiceWithMultiplePublicCtorsWithParams>();

        var di = container.BuildResolver();

        // Act
        

        //Assert
        Assert.Throws<Exception>(() => di.Get<IServiceWithMultiplePublicCtorsWithParams>());
    }

    [Fact]
    public void Should_CreateCertainInstance()
    {
        // Arrange
        var s = new ServiceWithDefaultValue();
        s.SetValue = 123;
        
        var container = new DummyDiContainer();
        container.Add<IServiceWithDefaultValue>(_ => s);
        
        var di = container.BuildResolver();

        // Act
        var service = di.Get<IServiceWithDefaultValue>();

        //Assert
        Assert.NotNull(service);
        Assert.Equal(123, service.GetValue());
    }
    
    [Fact]
    public void Should_ResolveEverytime_WithFactory()
    {
        // Arrange
        ServiceWithCtorCounter.Reset();
        
        var container = new DummyDiContainer();
        container.Add<IServiceWithCtorCounter>(_ => new ServiceWithCtorCounter());
        
        var di = container.BuildResolver();

        // Act
        var service = (ServiceWithCtorCounter)di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();

        //Assert
        Assert.NotNull(service);
        Assert.Equal(3, service.GetCounter());
    }
    
    [Fact]
    public void Should_CreateCertainInstance_Singleton()
    {
        // Arrange
        ServiceWithCtorCounter.Reset();

        var container = new DummyDiContainer();
        container.AddSingleton<IServiceWithCtorCounter>(_ => new ServiceWithCtorCounter());
        
        var di = container.BuildResolver();

        // Act
        var service = (ServiceWithCtorCounter)di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();
        di.Get<IServiceWithCtorCounter>();
        
        //Assert
        Assert.NotNull(service);
        Assert.Equal(1, service.GetCounter());
    }

    [Fact]
    public void Should_ReturnCertainInstance()
    {
        // Arrange
        var container = new DummyDiContainer();
        container.Add<IService>(typeof(Service));
        
        var di = container.BuildResolver();

        // Act
        var service = di.Get<IService>();

        //Assert
        Assert.NotNull(service);
    }
    
    [Fact]
    public void Should_ReturnDelegate()
    {
        // Arrange
        var container = new DummyDiContainer();
        container.AddSingleton<Func<int>>(() => 10);
        
        var di = container.BuildResolver();

        // Act
        var func = di.Get<Func<int>>();
        var result = func();
        
        //Assert
        Assert.NotNull(func);
        Assert.Equal(10, result);
    }
}