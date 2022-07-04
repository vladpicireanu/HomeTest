using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetBookReadRateQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetBookReadRateQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRateQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookReadRateQuery getBookReadRateQuery,
            GetBookReadRateQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetBookByIdResponse()));

            // Act
            var result = await sut.Handle(getBookReadRateQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetBookById(
                    It.Is<int>(x =>
                        x.Equals(getBookReadRateQuery.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRateQueryHandler_GrpcClientCalledTwice(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookReadRateQuery getBookReadRateQuery,
            GetBookReadRateResponse getBookReadRateResponse,
            GetBookByIdResponse getBookByIdResponse,
            GetBookReadRateQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookByIdResponse));

            coreLibraryGrpcClientMock.Setup(x => x.GetBookReadRate(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookReadRateResponse));

            // Act
            var result = await sut.Handle(getBookReadRateQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetBookById(
                    It.Is<int>(x =>
                        x.Equals(getBookReadRateQuery.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());

            coreLibraryGrpcClientMock.Verify(mock => mock.GetBookReadRate(
                    It.Is<int>(x =>
                        x.Equals(getBookReadRateQuery.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRateQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookReadRateQuery getBookReadRateQuery,
            GetBookReadRateResponse getBookReadRateResponse,
            GetBookByIdResponse getBookByIdResponse,
            GetBookReadRateQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookByIdResponse));

            coreLibraryGrpcClientMock.Setup(x => x.GetBookReadRate(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookReadRateResponse));


            // Act
            var result = await sut.Handle(getBookReadRateQuery, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(getBookReadRateResponse.BookReadRate, result.Response?.BookReadRate);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRateQueryHandler_ReturnsErrorNotFound(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookReadRateQuery getBookReadRateQuery,
            GetBookReadRateQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetBookByIdResponse());

            // Act
            var result = await sut.Handle(getBookReadRateQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetBookReadRateNotFound, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetBookReadRateNotFound, result.Error.Message);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRateQueryHandler_ReturnsErrorNoData(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookReadRateQuery getBookReadRateQuery,
            GetBookByIdResponse getBookByIdResponse,
            GetBookReadRateQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookByIdResponse));

            coreLibraryGrpcClientMock.Setup(x => x.GetBookReadRate(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetBookReadRateResponse { BookReadRate = default }));

            // Act
            var result = await sut.Handle(getBookReadRateQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetBookReadRateNoData, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetBookReadRateNoData, result.Error.Message);
        }
    }
}
