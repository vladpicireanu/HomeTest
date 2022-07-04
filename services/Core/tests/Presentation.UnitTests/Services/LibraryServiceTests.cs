using Application.Library.Dto;
using Application.Library.Queries;
using AutoFixture.Xunit2;
using Grpc.Core;
using MapsterMapper;
using MediatR;
using Moq;
using Presentation.Services;
using Presentation.UnitTests.Shared.Attributes;
using Xunit;

namespace Presentation.UnitTests.Services
{
    public class LibraryServiceTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetBookAvailability_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetBookAvailabilityResponse response,
            GetBookAvailabilityRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetBookAvailabilityQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetBookAvailability(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetBookAvailabilityQuery>(data =>
                        data.BookId.Equals(request.BookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookById_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetBookByIdResponse response,
            GetBookByIdRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetBookByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetBookById(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetBookByIdQuery>(data =>
                        data.BookId.Equals(request.BookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetMostBorrowedBooks_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetMostBorrowedBooksResponse response,
            GetMostBorrowedBooksRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetMostBorrowedBooksQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetMostBorrowedBooks(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetMostBorrowedBooksQuery>(data =>
                        data.TopRange.Equals(request.TopRange)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUsersWithMostRents_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetUsersWithMostRentsResponse response,
            GetUsersWithMostRentsRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersWithMostRentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetUsersWithMostRents(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetUsersWithMostRentsQuery>(data =>
                        data.TopRange.Equals(request.TopRange)
                        && data.StartDate.Equals(request.StartDate.ToDateTimeOffset())
                        && data.ReturnDate.Equals(request.ReturnDate.ToDateTimeOffset())),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUserRents_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetUserRentsResponse response,
            GetUserRentsRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetUserRentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetUserRents(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetUserRentsQuery>(data =>
                        data.UserId.Equals(request.UserId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetOtherBooks_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetOtherBooksResponse response,
            GetOtherBooksRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetOtherBooksQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetOtherBooks(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetOtherBooksQuery>(data =>
                        data.BookId.Equals(request.BookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetBookReadRate_MediatorSendCalledOnce(
            [Frozen] Mock<IMapper> mapperMock,
            [Frozen] Mock<IMediator> mediatorMock,
            GetBookReadRateResponse response,
            GetBookReadRateRequest request,
            ServerCallContext serverCallContext)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<GetBookReadRateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var sut = new LibraryService(mediatorMock.Object, mapperMock.Object);

            // Act
            await sut.GetBookReadRate(request, serverCallContext);

            // Assert
            mediatorMock.Verify(mock => mock.Send(It.Is<GetBookReadRateQuery>(data =>
                        data.BookId.Equals(request.BookId)),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
