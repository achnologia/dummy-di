// See https://aka.ms/new-console-template for more information

using DummyDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var s = new ServiceWithDefaultValue();
s.SetValue = 123;
        
var container = new ServiceCollection();
container.AddSingleton(s);

Console.WriteLine("Hello, World!");