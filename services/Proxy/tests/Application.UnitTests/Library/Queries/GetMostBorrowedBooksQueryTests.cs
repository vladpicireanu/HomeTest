using Application.Abstractions;
using Application.Library.Dto.Responses;
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
        public async Task GetMostBorrowedBooksQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetMostBorrowedBooksQuery getMostBorrowedBooksQuery,
            GetMostBorrowedBooksResponse getMostBorrowedBooksResponse,
            GetMostBorrowedBooksQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetMostBorrowedBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getMostBorrowedBooksResponse));

            // Act
            var result = await sut.Handle(getMostBorrowedBooksQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetMostBorrowedBooks(
                    It.Is<int>(x =>
                        x.Equals(getMostBorrowedBooksQuery.TopRange)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetMostBorrowedBooksQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetMostBorrowedBooksQuery getMostBorrowedBooksQuery,
            GetMostBorrowedBooksResponse getMostBorrowedBooksResponse,
            GetMostBorrowedBooksQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetMostBorrowedBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getMostBorrowedBooksResponse);

            // Act
            var result = await sut.Handle(getMostBorrowedBooksQuery, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(getMostBorrowedBooksResponse.Books.Count, result.Response?.Books.Count);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetMostBorrowedBooksQueryHandler_ReturnsError(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetMostBorrowedBooksQuery getMostBorrowedBooksQuery,
            GetMostBorrowedBooksQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetMostBorrowedBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetMostBorrowedBooksResponse { Books = new List<Models.Book>() });

            // Act
            var result = await sut.Handle(getMostBorrowedBooksQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetMostBorrowedBooksRangeLarge, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetMostBorrowedBooksRangeLarge, result.Error.Message);
        }
    }
}
