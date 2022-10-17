using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Repositories
{
    public class StreamerRepository: RepositoryBase<Streamer>, IStreamerRepository
    {
        public StreamerRepository(StreamerDbContext context)
            : base(context)
        {

        }
    }
}
