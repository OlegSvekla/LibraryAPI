# "CRUD Web API for Library Simulation"
Built using .NET Core, EF Core, MS SQL Server, AutoMapper, Authentication via bearer token with IdentityServer, Swagger and EF Fluent API.
# How to start a project:
- Clone this repository to your local compucter.
- Run the project (the application checks if the database provider is SQL Server and applies any pending migrations, initial test data will be seeded).
- You need to obtain a jwtToken before requests to authentucated endpoints :
	1) Registration (call the register endpoint)
	2) Email verification (call the verify-email endpoint and enter the verification token received in your email)
	3) Log in (enter your email and password)
	4) Enter the JwtToken received after authorization (call Available authorizations => Bearer <Your received Token>)
- Use API testing tools to interact with the endpoints and perform CRUD operations on entities.