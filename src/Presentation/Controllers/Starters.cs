using System;
using Application.StarterTasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Starters: Controller
    {
        private readonly ILogger<Starters> logger;
        private IMediator mediator;

        public Starters(ILogger<Starters> logger, IMediator mediator)
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

            if (!response)
            {
                return BadRequest();
            }
            return Ok();
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

    }
}
