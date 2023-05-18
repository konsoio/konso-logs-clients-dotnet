# Konso Logging .Net Client

The Konso Logging .NET Client Library is a powerful logging tool designed to simplify the process of capturing logs, enabling easy debugging, and centralizing log data within .NET applications. With its intuitive interface and comprehensive features, this library provides developers with a seamless logging experience.

## Features

**Logging Capabilities**: Konso Logging allows developers to capture logs seamlessly within their .NET applications. By integrating this library into your codebase, you gain the ability to log various events, messages, errors, and exceptions that occur during runtime. These logs serve as valuable diagnostic information, aiding in the identification and resolution of issues within your application..

**Easy Debugging**: Debugging is made effortless with Konso Logging. By strategically placing log statements throughout your code, you can gain insights into the execution flow and pinpoint areas of interest. This enables developers to effectively trace the path of execution, track variable values, and identify potential bottlenecks or areas for optimization.

**Centralized Log Data**: The library facilitates centralized log data management, allowing you to store and organize logs in a centralized location. This centralized approach simplifies log data exploration, as it eliminates the need to sift through individual log files scattered across different systems or machines. By consolidating log data, you can easily search, filter, and analyze logs, gaining valuable insights into application behavior and performance..

**Configurable Options**: Konso Logging provides flexible configuration options to adapt to your specific logging requirements. Developers can customize log levels tailoring the logging experience to suit their application's needs.

## Getting Started

To start using the Konso Logging .NET Client Library, simply follow the steps below:

⚠️ Prerequisites: *Konso account and created bucket*

### Install the library via NuGet or by manually referencing the assembly in your project

```

NuGet\Install-Package Konso.Clients.Logging

```

### Initialize the library with your API credentials and configuration settings

Add config to `appsettings.json`:

```json
"Konso": {
    "Logging": {
        "Endpoint": "https://apis.konso.io",
        "BucketId": "<your bucket id>",
        "ApiKey": "<bucket's access key>",
        "App": "api",
        "Level": "Information"
    }
}
```

in `startup.cs`:

```csharp

.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.AddKonsoLogger(options => {
        options.Endpoint = hostContext.Configuration.GetValue<string>("Konso:Logging:Endpoint");
        options.BucketId = hostContext.Configuration.GetValue<string>("Konso:Logging:BucketId");
        options.AppName = hostContext.Configuration.GetValue<string>("Konso:Logging:App");
        options.ApiKey = hostContext.Configuration.GetValue<string>("Konso:Logging:ApiKey");
    });
})

```

The library uses HttpContext to get scoped data such as request header or session id:

```csharp
.ConfigureServices((hostContext, services) =>
{
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
})
```

### Begin capturing logs using the .NET abstracted models 

Resolve the service in class constructor:

```csharp

public SomeController(ILogger<SomeController> logger)
{
    logger.Info("Welcome to TestApp");
}
```

## Requirements

- .NET 5 or higher
- Konso BucketId and API key

## Support and Feedback

If you encounter any issues or have any questions or feedback, please reach out to our support team at <support at konso.io>. We are here to assist you and continually improve the Konso Value Tracking .NET Client Library to meet your business needs.

✅ Developed / Developing by [InDevLabs](https://indevlabs.de)
