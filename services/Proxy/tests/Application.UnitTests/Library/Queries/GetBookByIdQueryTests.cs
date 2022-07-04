using Application.Abstractions;
using Application.Library.Dto.Responses;
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
        public async Task GetBookByIdQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookByIdQuery getBookByIdQuery,
            GetBookByIdResponse getBookByIdResponse,
            GetBookByIdQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookByIdResponse));

            // Act
            var result = await sut.Handle(getBookByIdQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetBookById(
                    It.Is<int>(x =>
                        x.Equals(getBookByIdQuery.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookByIdQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookByIdQuery getBookByIdQuery,
            GetBookByIdResponse getBookByIdResponse,
            GetBookByIdQueryHandler sut)
        {
            // Arrange
            getBookByIdResponse.Book.BookId = getBookByIdQuery.BookId;
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getBookByIdResponse);

            // Act
            var result = await sut.Handle(getBookByIdQuery, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(getBookByIdResponse.Book.BookId, result.Response?.Book.BookId);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookByIdQueryHandler_ReturnsError(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookByIdQuery getBookByIdQuery,
            GetBookByIdQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetBookByIdResponse());

            // Act
            var result = await sut.Handle(getBookByIdQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetBookByIdNotFound, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetBookByIdNotFound, result.Error.Message);
        }
    }
}
