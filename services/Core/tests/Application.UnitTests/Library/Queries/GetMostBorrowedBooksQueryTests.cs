using Application.Abstractions;
using Application.Library.Queries;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetMostBorrowedBooksQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetMostBorrowedBooksQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetMostBorrowedBooksQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetMostBorrowedBooksQuery query,
            List<Book> response,
            GetMostBorrowedBooksQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetMostBorrowedBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetMostBorrowedBooks(
                    It.Is<int>(x =>
                        x.Equals(query.TopRange)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
