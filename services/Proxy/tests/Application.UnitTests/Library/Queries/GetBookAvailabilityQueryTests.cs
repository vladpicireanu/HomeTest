using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetBookAvailabilityQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetBookAvailabilityQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookAvailabilityQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookAvailabilityQuery getBookAvailabilityQuery,
            GetBookAvailabilityResponse getBookAvailabilityResponse,
            GetBookAvailabilityQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetBookAvailability(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getBookAvailabilityResponse));

            // Act
            var result = await sut.Handle(getBookAvailabilityQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetBookAvailability(
                    It.Is<int>(x =>
                        x.Equals(getBookAvailabilityQuery.BookId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookAvailabilityQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookAvailabilityQuery getBookAvailabilityQuery,
            GetBookAvailabilityResponse getBookAvailabilityResponse,
            GetBookAvailabilityQueryHandler sut)
        {
            // Arrange
            getBookAvailabilityResponse.Book.BookId = getBookAvailabilityQuery.BookId;
            coreLibraryGrpcClientMock.Setup(x => x.GetBookAvailability(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getBookAvailabilityResponse);

            // Act
            var result = await sut.Handle(getBookAvailabilityQuery, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(getBookAvailabilityResponse.Book.BookId, result.Response.Book.BookId);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookAvailabilityQueryHandler_ReturnsError(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetBookAvailabilityQuery getBookAvailabilityQuery,
            GetBookAvailabilityQueryHandler sut)
        {
            // Arrange
            var expectedResult = new GenericResponse<GetBookAvailabilityResponse>(new Error
            {
                ErrorCode = ErrorCode.GetBookAvailabilityNotFound,
                Message = ErrorMessage.GetBookAvailabilityNotFound
            });

            coreLibraryGrpcClientMock.Setup(x => x.GetBookAvailability(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetBookAvailabilityResponse() { Book = new BookAvailability() });

            // Act
            var result = await sut.Handle(getBookAvailabilityQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetBookAvailabilityNotFound, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetBookAvailabilityNotFound, result.Error.Message);
        }
    }
}
