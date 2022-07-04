using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Domain;
using Moq;
using Xunit;
using static Application.Library.Queries.GetUsersWithMostRentsQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetUsersWithMostRentsQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetUsersWithMostRentsQueryHandler_GrpcClientCalledOnce(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetUsersWithMostRentsQuery query,
            GetUsersWithMostRentsResponse response,
            GetUsersWithMostRentsQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetUsersWithMostRents(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            coreLibraryGrpcClientMock.Verify(mock => mock.GetUsersWithMostRents(
                    It.Is<int>(topRange =>
                        topRange.Equals(query.TopRange)),
                    It.Is<DateTimeOffset>(startDate =>
                        startDate.Equals(query.StartDate)),
                    It.Is<DateTimeOffset>(returnDate =>
                        returnDate.Equals(query.ReturnDate)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUsersWithMostRentsQueryHandler_CorrectResponse(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetUsersWithMostRentsQuery query,
            GetUsersWithMostRentsResponse response,
            GetUsersWithMostRentsQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetUsersWithMostRents(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasError());
            Assert.Equal(response.Users.Count, result.Response?.Users.Count);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUsersWithMostRentsQueryHandler_ReturnsError(
            [Frozen] Mock<ICoreLibraryGrpcClient> coreLibraryGrpcClientMock,
            GetUsersWithMostRentsQuery query,
            GetUsersWithMostRentsQueryHandler sut)
        {
            // Arrange
            coreLibraryGrpcClientMock.Setup(x => x.GetUsersWithMostRents(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetUsersWithMostRentsResponse { Users = new List<UserMostRents>() });

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.HasError());
            Assert.True(result.Response is null);
            Assert.Equal(ErrorCode.GetUsersWithMostRentsRangeLarge, result.Error.ErrorCode);
            Assert.Equal(ErrorMessage.GetUsersWithMostRentsRangeLarge, result.Error.Message);
        }
    }
}
