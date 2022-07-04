using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Application.Models;
using AutoFixture.Xunit2;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Presentation.Controllers;
using Presentation.UnitTests.Shared.Attributes;
using Xunit;

namespace Presentation.UnitTests.Controllers
{
    public class LibraryControllerTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookAvailability_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetBookAvailabilityResponse> genericResponse,
            int bookId,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetBookAvailabilityQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetBookAvailability(bookId, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetBookAvailabilityQuery>(data =>
                        data.BookId.Equals(bookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookById_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetBookByIdResponse> genericResponse,
            int bookId,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetBookByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetBookById(bookId, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetBookByIdQuery>(data =>
                        data.BookId.Equals(bookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetMostBorrowedBooks_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetMostBorrowedBooksResponse> genericResponse,
            int topRange,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetMostBorrowedBooksQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetMostBorrowedBooks(topRange, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetMostBorrowedBooksQuery>(data =>
                        data.TopRange.Equals(topRange)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUsersWithMostRents_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetUsersWithMostRentsResponse> genericResponse,
            int topRange,
            DateTimeOffset startDate,
            DateTimeOffset returnDate,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersWithMostRentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetUsersWithMostRents(topRange, startDate, returnDate, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetUsersWithMostRentsQuery>(data =>
                        data.TopRange.Equals(topRange)
                        && data.StartDate.Equals(startDate)
                        && data.ReturnDate.Equals(returnDate)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUserRents_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetUserRentsResponse> genericResponse,
            int userId,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetUserRentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetUserRents(userId, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetUserRentsQuery>(data =>
                        data.UserId.Equals(userId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetOtherBooks_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetOtherBooksResponse> genericResponse,
            int bookId,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetOtherBooksQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetOtherBooks(bookId, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetOtherBooksQuery>(data =>
                        data.BookId.Equals(bookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRate_MediatorSendCalledOnce(
            [Frozen] Mock<ILogger<LibraryController>> loggerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GenericResponse<GetBookReadRateResponse> genericResponse,
            int bookId,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetBookReadRateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genericResponse);

            var sut = new LibraryController(loggerMock.Object, mediatorMock.Object);

            // Act
            await sut.GetBookReadRate(bookId, serverCallContext.CancellationToken);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetBookReadRateQuery>(data =>
                        data.BookId.Equals(bookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
