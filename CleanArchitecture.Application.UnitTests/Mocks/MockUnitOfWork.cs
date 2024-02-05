using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<StreamerDbContext>()
                .UseInMemoryDatabase(databaseName: $"StreamerDbContext- {dbContextId}")
                .Options;

            var streamerDbContextFake = new StreamerDbContext(options);

            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitofWork = new Mock<UnitOfWork>(streamerDbContextFake);



            return mockUnitofWork;

        }
    }
}
