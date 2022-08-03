using Paramore.Brighter;
using Polly.Registry;
using WebApplication1;

var handlerType = typeof(ExampleHandler);

var builder = WebApplication.CreateBuilder(args);
builder
.Services
.AddScoped(handlerType)
.AddSingleton(SubscriberRegistryFactory.CreateRegistry(new List<Type>() { handlerType }))
.AddScoped<IAmAHandlerFactory, CommandHandlerFactory>()
.AddScoped<IAmARequestContextFactory, InMemoryRequestContextFactory>()
.AddScoped<IPolicyRegistry<string>, DefaultPolicy>()
.AddScoped<IAmACommandProcessor, OurCommandProcessor>();


var app = builder
    .Build();

app.MapGet("/", (IAmACommandProcessor commandProcessor) => commandProcessor.Send(new ExampleCommand()));

app.Run();
