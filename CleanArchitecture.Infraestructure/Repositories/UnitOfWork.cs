using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infraestructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private Hashtable repositories;
        private readonly StreamerDbContext context;

        public UnitOfWork(StreamerDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
           context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
           if(repositories == null)
            {
                repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if(!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(IAsyncRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),context);
                
                repositories.Add(type, repositoryInstance);
            
            }
            return (IAsyncRepository<TEntity>)repositories[type];



        }
    }
}
