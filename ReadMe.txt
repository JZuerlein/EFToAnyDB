An example of how to setup EF Core migrations for multiple database providers.

EFToAnyDB.Program.cs configures services and a hosted service that applies the migrations.

Depending on the provider specified in the app.config file, the services for:

	SQLServer,
	SQLite

will be added.

When the hosted service runs, it will look to see if there are migrations to apply, and apply them.

EFToAnyDB.Data project contains the definiton of the database context.

EFToAnyDB.SQLServer project contains the extensions to add the database context and define where the SQLServer migrations are located.
The DBContextFactory class is provided to create migrations from the PackageManager console.

EFToAnyDB.SQLite project containts the extensions to add the database context and define where the SQLite migrations are located.
The DBContextFactory class is provided to create migrations from the PackageManager console.