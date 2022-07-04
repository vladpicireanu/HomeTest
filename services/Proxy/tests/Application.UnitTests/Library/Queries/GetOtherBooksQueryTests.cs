using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Application.Models;
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
        public async Task GetOtherBooksQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetOtherBooksQuery getOtherBooksQuery,
            GetOtherBooksResponse getOtherBooksResponse,
            GetOtherBooksQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetOtherBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getOtherBooksResponse));

            // Act
            var result = await sut.Handle(getOtherBooksQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetOtherBooks(
                    It.Is<int>(x =>
                        x.Equals(getOtherBooksQuery.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetOtherBooksQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetOtherBooksQuery getOtherBooksQuery,
            GetOtherBooksResponse getOtherBooksResponse,
            GetOtherBooksQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetOtherBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getOtherBooksResponse);

            // Act
            var result = await sut.Handle(getOtherBooksQuery, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(getOtherBooksResponse.Books.Count, result.Response?.Books.Count);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetOtherBooksQueryHandler_ReturnsError(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetOtherBooksQuery getOtherBooksQuery,
            GetOtherBooksQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetOtherBooks(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetOtherBooksResponse { Books = new List<Book>() });

            // Act
            var result = await sut.Handle(getOtherBooksQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetOtherBooksNoData, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetOtherBooksNoData, result.Error.Message);
        }
    }
}
