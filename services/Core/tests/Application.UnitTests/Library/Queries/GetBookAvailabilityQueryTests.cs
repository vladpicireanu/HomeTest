using Application.Abstractions;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Moq;
using Xunit;
using static Application.Library.Queries.GetBookAvailabilityQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetBookAvailabilityQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookAvailabilityQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetBookAvailabilityQuery query,
            BookAvailability response,
            GetBookAvailabilityQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetBookAvailability(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetBookAvailability(
                    It.Is<int>(x =>
                        x.Equals(query.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
