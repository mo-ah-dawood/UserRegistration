var builder = DistributedApplication.CreateBuilder(args);


// ! This section requires docker desktop 
// var db_password = builder.AddParameter("DB-PASSWORD", secret: true);
// var db = builder.AddSqlServer("DB-SERVER", db_password)
//                  .WithLifetime(ContainerLifetime.Persistent)
//                  .AddDatabase("DB");

var api = builder.AddProject<Projects.CvSystem_Api>("api");

builder.AddNpmApp("frontend", "../../Frontend")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
