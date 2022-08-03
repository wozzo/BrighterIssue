using Paramore.Brighter;
using Polly.Registry;

namespace WebApplication1;

public static class SubscriberRegistryFactory
{
    public static IAmASubscriberRegistry CreateRegistry(IEnumerable<Type> handlerTypes)
    {
        var registry = new SubscriberRegistry();

        foreach (var handlerType in handlerTypes)
        {
            var requestType = handlerType.BaseType.GenericTypeArguments.Single();
            registry.Add(requestType, handlerType);
        }

        return registry;
    }
}

public class CommandHandlerFactory : IAmAHandlerFactory
{
    public IHandleRequests Create(Type handlerType)
    {
        throw new NotImplementedException();
    }

    public void Release(IHandleRequests handler)
    {
        throw new NotImplementedException();
    }
}

public class OurCommandProcessor : CommandProcessor
{
    public OurCommandProcessor(
        IAmASubscriberRegistry subscriberRegistry,
        IAmAHandlerFactory handlerFactory,
        IAmARequestContextFactory requestContextFactory,
        IPolicyRegistry<string> policyRegistry
    ) : base(subscriberRegistry, handlerFactory, requestContextFactory, policyRegistry)
    { }
}

public class ExampleCommand : ICommand
{
    public Guid Id { get; set; }
}

public class ExampleHandler : RequestHandler<ExampleCommand>
{
    public override ExampleCommand Handle(ExampleCommand command)
    {
        throw new NotImplementedException();
    }
}