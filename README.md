## This solution is based on:

Jason Taylor and Mukesh Murugan
<br/>
https://github.com/jasontaylordev/CleanArchitecture
<br/>
https://github.com/iammukeshm/CleanArchitecture.WebApi

## Run Application
For simple run, or from Visual Studio run WebApi with kestrel or type ''dotnet run'' on your terminal at folder "src/Presentation/WebApi". 
It will run with InMemory DbContext.

For full experience (logging, database, redis, tracing, metrics, replicas), run it with Project Tye. Getting Started [here](https://github.com/dotnet/tye/blob/main/docs/getting_started.md).
<br/>
On Solution Folder, on your shell of your choice type
```text
tye run
```

on your browser: https://localhost:5001/swagger/index.html


## Technologies
* .NET 5.0
* ASP.NET Core 5.0
* Entity Framework Core 5.0
* OpenId Connect
* Swagger
* Serilog
* CQRS with MediatR
* Fluent Validation
* Health Checks
* Prometheus-net
* OpenTelemetry
* Project Tye

## License

This project is licensed with the [MIT license](LICENSE).

