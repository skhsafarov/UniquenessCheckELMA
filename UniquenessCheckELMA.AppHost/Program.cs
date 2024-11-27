var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UniquenessCheckELMA_Application>("uniquenesscheckelma");

builder.Build().Run();
