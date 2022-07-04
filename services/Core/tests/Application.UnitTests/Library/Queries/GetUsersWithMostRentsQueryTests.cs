using Application.Abstractions;
using Application.Library.Queries;
using Application.Models;
using Application.UnitTests.Shared.Attributes;
using AutoFixture.Xunit2;
using Moq;
using Xunit;
using static Application.Library.Queries.GetUsersWithMostRentsQuery;

namespace Application.UnitTests.Library.Queries
{
    public class GetUsersWithMostRentsQueryTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetUsersWithMostRentsQueryHandler_RepoCalledOnce(
            [Frozen] Mock<ILibraryRepository> repoMock,
            GetUsersWithMostRentsQuery query,
            List<UserMostRents> response,
            GetUsersWithMostRentsQueryHandler sut)
        {
            // Arrange
            repoMock.Setup(x => x.GetUsersWithMostRents(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            repoMock.Verify(mock => mock.GetUsersWithMostRents(
                    It.Is<int>(topRange =>
                        topRange.Equals(query.TopRange)),
                    It.Is<DateTimeOffset>(startDate =>
                        startDate.Equals(query.StartDate)),
                    It.Is<DateTimeOffset>(returnDate =>
                        returnDate.Equals(query.ReturnDate)),
                    It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
