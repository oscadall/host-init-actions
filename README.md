# Host init actions

This is a simple library for defining asynchronous operations to be performed before the application starts. Typically, it is an asynchronous initialization of singleton services registered in an IoC container. This means that there is no need to perform this initialization in a blocking manner before registering to the IoC container. HostInitActions are based on the IHostedService implementation, which means they only work in IHost implementation environments that support the IHostedService work flow. For example, within a regular WebHost in an ASP.NET Core application.

## Registration of initialization actions

Registration of initialization actions is done in the ```ConfigureServices``` method of the host builder or ```Startup``` class.

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureServices(services =>
{
    services.AddSingleton<IDatabaseService, DatabaseService>();
	...
    services.AddAsyncServiceInitialization()
        .AddInitAction<IDatabaseService>(async (databaseService, cancellationToken) =>
        {
            await databaseService.CreateIfNotExistsAsync(cancellationToken);
        });
});
```

First, it is necessary to register the initialization service using the  ```AddAsyncServiceInitialization``` method which returns a collection for registering specific init actions that will be executed before the application starts. 

The ```AddInitAction```  method expects the generic parameter of the service type that is required for initialization and also the function that executes the required init action on this service. The initialized service will be retrieved from the IoC Container and passed as a parameter to the initialization function along with the optional CancellationToken.

You can define multiple init actions and they will be executed sequentially in the order of registration.

```csharp
 services.AddAsyncServiceInitialization()
        .AddInitAction<IDatabaseService>(async (databaseService, cancellationToken) =>
        {
            await databaseService.CreateIfNotExistsAsync(cancellationToken);
        })
        .AddInitAction<IDeviceClient>(async deviceClient =>
        {
            await deviceClient.InitializeAsync();
        });
```

It is also possible to define more services (max 5) for one init action in case you need to have better control over the execution of individual initializations.

```csharp
 services.AddAsyncServiceInitialization()
        .AddInitAction<IDatabaseService, IDeviceClient>(async (databaseService, deviceClient) =>
        {
            await deviceClient.PreInitializeAsync();
            await Task.WhenAll(new []
            {
                deviceClient.InitializeAsync(),
                databaseService.CreateIfNotExistsAsync(),
            });
            await deviceClient.ConnectToDeviceAsync(port: 1223);
        });
```

## Initialization executors

Another way to perform an initialization action with multiple dependencies is the custom implementation of the ```IAsyncInitActionExecutor``` interface. This requires implementing only the ```ExecuteAsync``` method that executes your initialization logic. The dependencies needed to perform the initialization action are defined as class dependencies using the constructor and are injected from the IoC container.

```csharp
    public class MyInitActionExecutor : IAsyncInitActionExecutor
    {
        private readonly IDependency1 _d1;
        private readonly IDependency2 _d2;
        ...
        private readonly IConfig _config;

        public MyInitActionExecutor(IDependency1 d1, IDependency2 d2, ... , IConfig config) {...}

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _d1.SomeAsyncAction(_config.SomeValue);
            await _d2.OtherAsyncAction(_config.OtherValue);
            ...
        }
    }
```

Registering custom ```IAsyncInitActionExecutor``` is done using one of the ```AddInitActionExecutor``` method overloads on ```IInitActionCollection```.

```csharp
    services.AddAsyncServiceInitialization()
        .AddInitActionExecutor<MyInitActionExecutor>()
        .AddInitActionExecutor(otherExecutorInstance)
        .AddInitActionExecutor(serviceProvider => new LastExecutor(...));
```

You can freely combine registrations using ```AddInitAction``` and ```AddInitActionExecutor```. The execution order of initialization actions and initialization executors is still defined by the order of registration.

## Invocation of init actions

Init actions are executed before:
- The app's request processing pipeline is configured.
- The server is started and ```IApplicationLifetime.ApplicationStarted``` is triggered.
- The Kestrel is started (in WebHost)

## Explicit invocation of init actions

For applications or tests that do not run in the Host's environment but only use the SecviceCollection container to provide dependency injection, it is possible to explicitly invoke the execution of registered init actions directly on the ServiceProvider.

```csharp
    services.AddServicesThatRegisterSomeInitActionsInternally()
    ...
    var serviceProvider = services.BuildServiceProvider();

    // Performs all initialization actions, if any are registered.
    await serviceProvider.ExecuteInitActionsAsync();
    ...
```

Thanks to the ability to explicitly invoke init actions on the ```IServiceProvider```, you can take advantage of the abstraction offered by the library even if your application uses only ```ServiceCollection```. Also, in tests it is not necessary to run the ```Host``` instance to execute all init actions but you can simplify the test by simply using the ```ServiceCollection``` and the ```ExecuteInitActionsAsync``` method on the built ```IServiceProvider```.