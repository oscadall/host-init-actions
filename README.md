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

## Invocation of init actions

Init actions are executed before:
- The app's request processing pipeline is configured.
- The server is started and IApplicationLifetime.ApplicationStarted is triggered.