using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers.Any())
            {
                context.Streamers.AddRange(GetPreConfiguratedStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Se insertan datos de seed {context}",typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreConfiguratedStreamer()
        {
            return new List<Streamer> {
                new Streamer{CreatedBy="vaxiDrez",Nombre="Maxi HBP",Url="http://www.hbp.com"},
                new Streamer{CreatedBy="vaxiDrez",Nombre="Amazon VIP",Url="http://www.vip.com"}

            };
        }

    }
}
