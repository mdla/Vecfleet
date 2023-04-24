# Vecfleet
Evaluación técnica. Desarrollar una aplicación web que permita la gestión de vehículos.

# Configuracíon de la aplicación
- Cambiar la cadena de conexión a la base de datos. src/Vecfleet.Web/appsettings.json o src/Vecfleet.Web/appsettings.Development.json



# Pasos para ejecutar la aplicacion
Pasos para ejecutar la aplicacion.
Desde la carpeta root del proyecto ejecutar.
``` 
dotnet run --project .\Vecfleet.Web\Vecfleet.Web.csproj
```
Se inicializa la app con el siguiente output
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7128
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5221
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: E:\dev\net\Vecfleet\src\Vecfleet.Web
```
 Ingresar desde el navegador a la URL https://localhost:7128
 
 Una vez ingresado aparecera el mensaje:
 Launching the SPA proxy...
This page will automatically redirect to https://localhost:44420 when the SPA proxy is ready.

# Backend
Proyecto WebApi que inicializa una API en net Core y ademas levanta un proxy que ademas inicia la applicacion Frontend en React

URL de swagger con los endpoints de la API
https://localhost:7128/swagger/index.html

Nuggets utilizado
- MediatR
- Fluent validation
- Entity Framework Core

# Migraciones
Una vez que este corriendo la aplicacion se ejecutaran las migraciones automaticamente.

# Frontend
El proyecto en react se encuentra en **src/Vecfleet.Web/ClientApp**.
El proyecto se la inicia junto con la applicacion en backend para que sea mas comodo el desarrollo.
Para acceder al sitio WEB ingresar a la URL https://localhost:44420/

Paquetes utilizado
- React-table
- Zustand
- React-bootstrap
- Typescript

