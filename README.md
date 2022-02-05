Goal of ...
=====================
This is an open-source project written in .NET Core and Vue Js.

The goal of this repsoitory is to implement the most common used technologies in .Net Core backend and VueJs frontend.

This project is a template for creating a Single Page App (SPA) with Vue JavaScript and ASP.NET Core.

## Give a Star! :star:
If you liked the repo or if it helped you, a star would be appreciated.

## Features
* Multi Tenancy
* Multi Database providers (PostgreSql , SqlServer)
* Dynamic File uploading on file system or database
* Global Exception Handling
* Logging :
    1. All api actions are logged.
    2. All exceptions are logged.
* JWT Authentication
* .Net Core Identity
* Email Service

## Technologies

![develop-asp-dot-net-core-and-framework-mvc-web-api](https://user-images.githubusercontent.com/39926422/124902258-02652a80-dff8-11eb-9f45-e3c10d77a17f.png)

Backend :

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [MailKit](https://www.nuget.org/packages/MailKit/)
* [Serilog](https://serilog.net/)
* [SqlServer](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
* [PostgreSQL](https://www.postgresql.org/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

![vuejs_original_wordmark_logo_icon_146305](https://user-images.githubusercontent.com/39926422/124902999-c088b400-dff8-11eb-982f-0d8a95f8ccf1.png)

Frontend :

* [VueJs 3](https://vuejs.org/) 
* [Vue Router](https://router.vuejs.org/)
* [Vuex](https://next.vuex.vuejs.org/)
* [Axios](https://github.com/axios/axios)
* [mitt](https://github.com/developit/mitt)
* [bootstrap](https://getbootstrap.com/)

## Database Configuration
* Database provider :

    You can use SqlServer or PostgreSql in this template by update DatabaseProvider in **Api/appsettings.json** 
    ```json
      "DatabaseProvider": 1 /* Postgres = 1 , SqlServer = 2 */
    ```
    Then update ConnectionString 
    ```json
     "ConnectionStrings": {
        "SqlServer": "Server=.;Database=TestDb;User Id=YourDatabaseId;Password=YourDatabasePassword",
        "Postgres": "Host=localhost;Database=TestDb;Username=YourDatabaseUsername;Password=YourDatabasePassword"
      },
    ```

* Files Storing :
    There are two ways for storing files in this template :
    1. Store in Database
    2. Store in files

    ```json
      "StoreFilesOnDatabase": false,
    ```

## Backend Architecture Overview

This application uses Clean architecture and also can be used to build projects with Domain-Driven-Design approach.

### Core Layer
This will contains Enums , Exception classes , Interfaces (The Interfaces of generic repositories) and Abstractions (Implementions on above layers) , Setting classes , Entities , POCO classes, construction, and model validation , and Utilities will be used in above layers .

### Infrastructure Layer
This layer contains Entities , Database config , Migrations , DataProviders (Presentation layer uses DataProviders for getting proper data) and application services such as Domain services (Business logic) or other services like Email , Sms and etc.

### Presentation Layer (Backend API)

Api endpoints , Middlewares , FilterActions , Service Registrations are placed in this layer.
In addition, this layer depends on Infrastructure layer.

## Frontend Overview
Below image demonstrates the essence of the frontend and shows how it works :
![Frontend_Overview](https://user-images.githubusercontent.com/39926422/121818798-97e1f880-cc9e-11eb-944f-d20df0853c18.png)

### Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```
