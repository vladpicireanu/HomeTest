using Application.Abstractions;
using Application.Library.Queries;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetOtherBooksQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetOtherBooksQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetOtherBooksQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetOtherBooksQuery query,
            List<Book> response,
            GetOtherBooksQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetOtherBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetOtherBooks(
                    It.Is<int>(x =>
                        x.Equals(query.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
