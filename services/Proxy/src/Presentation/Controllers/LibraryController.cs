using Application.Library.Dto.Responses;
using Application.Library.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Response;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : BaseController
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
        [ProducesResponseType(typeof(GetBookByIdResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 404)]
        public async Task<ActionResult> GetBookById(int bookId, CancellationToken ct)
        {
            logger.LogInformation("Proxy - > receiving request : {bookId}", bookId);

            var response = await mediator.Send(new GetBookByIdQuery(bookId), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);

            if (response.HasError())
            {
                return NotFoundResponse(response.Error);
            }

            return Ok(response.Response);
        }

        [HttpGet]
        [Route("GetBookAvailability")]
        [ProducesResponseType(typeof(GetBookAvailabilityResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 404)]
        public async Task<ActionResult> GetBookAvailability(int bookId, CancellationToken ct)
        {
            logger.Log(LogLevel.Information, "Proxy - > receiving request : {bookId}", bookId);

            var response = await mediator.Send(new GetBookAvailabilityQuery(bookId), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);
            
            if (response.HasError())
            {
                return NotFoundResponse(response.Error);
            }

            return Ok(response.Response);
        }

        [HttpGet]
        [Route("GetMostBorrowedBooks")]
        [ProducesResponseType(typeof(GetMostBorrowedBooksResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 400)]
        public async Task<ActionResult> GetMostBorrowedBooks(int topRange, CancellationToken ct)
        {
            logger.Log(LogLevel.Information, "Proxy - > receiving request : {topRange}", topRange);

            var response = await mediator.Send(new GetMostBorrowedBooksQuery(topRange), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);

            if (response.HasError())
            {
                return BadRequestResponse(response.Error);
            }

            return Ok(response.Response);
        }

        [HttpGet]
        [Route("GetUsersWithMostRents")]
        [ProducesResponseType(typeof(GetUsersWithMostRentsResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 400)]
        public async Task<ActionResult> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate, CancellationToken ct)
        {
            logger.Log(LogLevel.Information, "Proxy - > receiving request : {topRange} {startDate} {returnDate}", topRange, startDate, returnDate);

            var response = await mediator.Send(new GetUsersWithMostRentsQuery(topRange, startDate, returnDate), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);

            if (response.HasError())
            {
                return BadRequestResponse(response.Error);
            }

            return Ok(response.Response);
        }

        [HttpGet]
        [Route("GetUserRents")]
        [ProducesResponseType(typeof(GetUserRentsResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 400)]
        public async Task<ActionResult> GetUserRents(int userId, CancellationToken ct)
        {
            logger.Log(LogLevel.Information, "Proxy - > receiving request : {userId}", userId);

            var response = await mediator.Send(new GetUserRentsQuery(userId), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);

            if (response.HasError())
            {
                return BadRequestResponse(response.Error);
            }

            return Ok(response.Response);
        }

        [HttpGet]
        [Route("GetOtherBooks")]
        [ProducesResponseType(typeof(GetOtherBooksResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 400)]
        public async Task<ActionResult> GetOtherBooks(int bookId, CancellationToken ct)
        {
            logger.Log(LogLevel.Information, "Proxy - > receiving request : {bookId}", bookId);

            var response = await mediator.Send(new GetOtherBooksQuery(bookId), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);

            if (response.HasError())
            {
                return BadRequestResponse(response.Error);
            }

            return Ok(response.Response);
        }

        [HttpGet]
        [Route("GetBookReadRate")]
        [ProducesResponseType(typeof(GetBookReadRateResponse), 200)]
        [ProducesResponseType(typeof(GenericErrorPayload), 400)]
        [ProducesResponseType(typeof(GenericErrorPayload), 404)]
        public async Task<ActionResult> GetBookReadRate(int bookId, CancellationToken ct)
        {
            logger.Log(LogLevel.Information, "Proxy - > receiving request : {bookId}", bookId);

            var response = await mediator.Send(new GetBookReadRateQuery(bookId), ct);

            logger.Log(LogLevel.Information, "Proxy - > sending response : {response}", response);

            if (response.HasError())
            {
                if(response.Error.ErrorCode == ErrorCode.GetBookReadRateNotFound)
                    return NotFoundResponse(response.Error);

                return BadRequestResponse(response.Error);
            }

            return Ok(response.Response);
        }
    }
}
