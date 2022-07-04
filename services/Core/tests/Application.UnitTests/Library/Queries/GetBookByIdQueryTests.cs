using Application.Abstractions;
using Application.Library.Queries;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetBookByIdQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetBookByIdQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookByIdQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetBookByIdQuery query,
            Book response,
            GetBookByIdQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetBookById(
                    It.Is<int>(x =>
                        x.Equals(query.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
