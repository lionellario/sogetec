namespace Sogetec.AppHost.Extensions.Infrastructure;

internal static partial class DockerExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        public IResourceBuilder<IResourceWithConnectionString> AddRedisServer(string name)
        {
            if (builder.ExecutionContext.IsRunMode)
            {
                return builder
                    .AddRedis(name)
                    .WithRedisInsight()
                    .WithDataVolume()
                    .WithImagePullPolicy(ImagePullPolicy.Always)
                    .WithLifetime(ContainerLifetime.Persistent);
            }

            // In production these come from environment variables on the VPS.
            return builder
                    .AddConnectionString(name);
        }
    }
}
