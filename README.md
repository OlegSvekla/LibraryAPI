# "CRUD Web API for Library Simulation"
Built using .NET Core, EF Core, MS SQL Server, AutoMapper, Authentication via bearer token with IdentityServer, Swagger and EF Fluent API.
# How to start a project:
- Clone this repository to your local compucter.
- Run the project (the application checks if the database provider is SQL Server and applies any pending migrations, initial test data will be seeded).
- You need to obtain a jwtToken before requests to authentucated endpoints (if you can't get the token for various reasons and therefore can't use the application service, just comment out the [Authorize] attribute in the LibraryController).
- Use API testing tools to interact with the endpoints and perform CRUD operations on entities.