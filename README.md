

# Konso Logging .Net Client

A .NET 5 Open Source Client Library for [KonsoApp](https://app.konso.io)

✅ Developed / Developing by [InDevLabs](https://indevlabs.de)


### Installation

⚠️ Prerequisites: *Konso account and created bucket* 

In order to use this library, you need reference it in your project.

Add config to `appsettings.json`:
```
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


Define logging:

```
.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.AddKonsoLogger();
})
```

Define config:
```
.ConfigureServices((hostContext, services) =>
{
    var _logsConfig = new KonsoLoggerConfig();
    _logsConfig.Endpoint = hostContext.Configuration.GetValue<string>("Konso:Logging:Endpoint");
    _logsConfig.BucketId = hostContext.Configuration.GetValue<string>("Konso:Logging:BucketId");
    _logsConfig.AppName = hostContext.Configuration.GetValue<string>("Konso:Logging:App");
    _logsConfig.ApiKey = hostContext.Configuration.GetValue<string>("Konso:Logging:ApiKey");

    services.AddSingleton(_logsConfig);
})
```
