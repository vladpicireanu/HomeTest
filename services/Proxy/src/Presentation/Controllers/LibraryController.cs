using Application.Library.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> logger;
        private IMediator mediator;

        public LibraryController(ILogger<LibraryController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("GetBookById")]
        public async Task<ActionResult> GetBookById(int bookId)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {bookId}");

            var response = await mediator.Send(new GetBookByIdQuery(bookId));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetBookAvailability")]
        public async Task<ActionResult> GetBookAvailability(int bookId)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {bookId}");

            var response = await mediator.Send(new GetBookAvailabilityQuery(bookId));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetMostBorrowedBooks")]
        public async Task<ActionResult> GetMostBorrowedBooks(int topRange)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {topRange}");

            var response = await mediator.Send(new GetMostBorrowedBooksQuery(topRange));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetUsersWithMostRents")]
        public async Task<ActionResult> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {topRange} {startDate} {returnDate}");

            var response = await mediator.Send(new GetUsersWithMostRentsQuery(topRange, startDate, returnDate));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetUserRents")]
        public async Task<ActionResult> GetUserRents(int userId)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {userId}");

            var response = await mediator.Send(new GetUserRentsQuery(userId));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetOtherBooks")]
        public async Task<ActionResult> GetOtherBooks(int bookId)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {bookId}");

            var response = await mediator.Send(new GetOtherBooksQuery(bookId));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetBookReadRate")]
        public async Task<ActionResult> GetBookReadRate(int bookId)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {bookId}");

            var response = await mediator.Send(new GetBookReadRateQuery(bookId));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }
    }
}
