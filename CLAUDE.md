# Claude Code Instructions

## Kommunikation
- Antworte auf Deutsch wenn der User auf Deutsch schreibt

## .NET REST API Design Patterns

### Controller
- Nur HTTP-Binding und Routing — keine Settings/Konfiguration injizieren
- Domain-Objekte direkt weitergeben, kein unnötiges Mapping
- `CancellationToken` in Actions verwenden (wird von ASP.NET aus `HttpContext.RequestAborted` befüllt)

### Service
- Enthält Business-Logik und Infrastruktur-Details (Serialisierung, Topic-Name etc.)
- Injiziert Settings — nicht der Controller
- Der Aufrufer muss nicht wissen wohin/wie publiziert wird

### Settings
- Konfiguration in einem Settings-Record bündeln
- Env-Vars in `Program.cs` lesen und in Settings-Objekt packen
- Als Singleton registrieren

### Azure Service Bus
- `ServiceBusClient` als Singleton registrieren
- `ServiceBusSettings` enthält `ConnectionString` und `TopicName`
- Service serialisiert das Domain-Objekt selbst (JSON) und kennt den TopicName intern

### OpenAPI / Swagger (.NET 10)
- Built-in: `AddOpenApi()` + `MapOpenApi()`
- UI: `Scalar.AspNetCore` → `app.MapScalarApiReference()` (nur im Development-Block)
