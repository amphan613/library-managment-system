var builder = DistributedApplication.CreateBuilder(args);

var libraryApi = builder.AddProject<Projects.library_api>("library-api");

var libraryUI= builder.AddProject<Projects.library_ui>("library-ui")
	.WithExternalHttpEndpoints()
	.WithReference(libraryApi);

builder.Build().Run();
