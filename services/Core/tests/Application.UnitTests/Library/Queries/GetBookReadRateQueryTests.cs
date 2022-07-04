using Application.Abstractions;
using Application.Library.Queries;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Moq;
using Xunit;
using static Application.Library.Queries.GetBookReadRateQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetBookReadRateQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRateQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetBookReadRateQuery query,
            int response,
            GetBookReadRateQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetBookReadRate(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetBookReadRate(
                    It.Is<int>(x =>
                        x.Equals(query.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
