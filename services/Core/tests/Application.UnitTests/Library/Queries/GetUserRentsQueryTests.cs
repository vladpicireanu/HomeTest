using Application.Abstractions;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Moq;
using Xunit;
using static Application.Library.Queries.GetUserRentsQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetUserRentsQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetUserRentsQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetUserRentsQuery query,
            List<UserRent> response,
            GetUserRentsQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetUserRents(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetUserRents(
                    It.Is<int>(x =>
                        x.Equals(query.UserId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
