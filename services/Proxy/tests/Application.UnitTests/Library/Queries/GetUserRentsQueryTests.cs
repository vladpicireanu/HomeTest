using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetUserRentsQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetUserRentsQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetOtherBooksQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetUserRentsQuery getUserRentsQuery,
            GetUserRentsResponse getUserRentsResponse,
            GetUserRentsQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetUserRents(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(getUserRentsResponse));

            // Act
            var result = await sut.Handle(getUserRentsQuery, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetUserRents(
                    It.Is<int>(x =>
                        x.Equals(getUserRentsQuery.UserId)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUserRentsQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetUserRentsQuery getUserRentsQuery,
            GetUserRentsResponse getUserRentsResponse,
            GetUserRentsQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetUserRents(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getUserRentsResponse);

            // Act
            var result = await sut.Handle(getUserRentsQuery, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(getUserRentsResponse.UserRents.Count, result.Response?.UserRents.Count);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUserRentsQueryHandler_ReturnsError(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetUserRentsQuery getUserRentsQuery,
            GetUserRentsQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetUserRents(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetUserRentsResponse { UserRents = new List<UserRent>() });

            // Act
            var result = await sut.Handle(getUserRentsQuery, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetUserRentsNoData, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetUserRentsNoData, result.Error.Message);
        }
    }
}
