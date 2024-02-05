using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repos;
        private readonly StreamerDbContext _dbContext;

        private  IVideoRepository _videoRepository;
        private  IStreamerRepository _streamerRepository;

        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_dbContext);
        public IStreamerRepository StreamerRepository => _streamerRepository ??= new StreamerRepository(_dbContext);

        public UnitOfWork(StreamerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public StreamerDbContext StreamerDbContext => _dbContext;

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repos == null)
            {
                _repos = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (!_repos.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repos.Add(type, repositoryInstance);
            }
            return (IAsyncRepository<TEntity>)_repos[type];
        }
    }
}
