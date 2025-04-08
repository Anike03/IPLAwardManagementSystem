# IPLAwardManagementSystem


## ANIKET SHARMA
I defined C# classes that represent the core entities of the application. These models act as the blueprint for our database tables. The ApplicationDbContext class was created by extending DbContext. This class is responsible for managing the database connection and serves as the central hub through which Entity Framework communicates with the database. I used Entity Framework Core’s migration tools to scaffold and apply database migrations. Migrations track changes in the models and automatically generate the SQL code needed to update the database schema.

## NEERAJ KUMAR
To promote clean architecture and separation of concerns, I defined interfaces for all core business logic components. Corresponding services were implemented to encapsulate business logic and handle operations such as data retrieval, manipulation, and communication with the database via the ApplicationDbContext. he project layout was structured using Razor Views (or Blazor components if applicable), ensuring consistency across all pages.