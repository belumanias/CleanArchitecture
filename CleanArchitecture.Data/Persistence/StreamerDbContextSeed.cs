using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContext> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Se estan insertando nuevos records a la db {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
            new Streamer{CreatedBy="Belumanias",Nombre="Spotify", Url="http://spotify.com"},
            new Streamer{CreatedBy="Belumanias",Nombre="Cartoon", Url="http://cartoon.com"}
            };
        }
    }
}
