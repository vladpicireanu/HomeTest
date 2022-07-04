using Application.StarterTasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StartersController: ControllerBase
    {
        private readonly ILogger<StartersController> logger;
        private IMediator mediator;

        public StartersController(ILogger<StartersController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("Two-Power")]
        public async Task<ActionResult> IsNumberTwoPower(int number)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request : {number}");

            var response = await mediator.Send(new IsNumberTwoPowerQuery(number));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("ReverseString")]
        public async Task<ActionResult> ReverseString(string text)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request: {text}");

            var response = await mediator.Send(new GetReverseStringQuery(text));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("ReplicateString")]
        public async Task<ActionResult> ReplicateString(string text, int replicas)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request: {text} {replicas}");

            var response = await mediator.Send(new GetReplicateStringQuery(text, replicas));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }

        [HttpGet]
        [Route("OddNumbers")]
        public async Task<ActionResult> OddNumbers(int startNumber, int stopNumber)
        {
            logger.Log(LogLevel.Information, $"Proxy - > receiving request: {startNumber} {stopNumber}");

            var response = await mediator.Send(new GetOddNumbersQuery(startNumber, stopNumber));

            logger.Log(LogLevel.Information, $"Proxy - > sending response : {response}");

            return Ok(response);
        }
    }
}
