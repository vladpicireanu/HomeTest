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
            logger.Log(LogLevel.Information, $"Proxy gateway - > receiving request with number : {number}");

            var response = await mediator.Send(new IsNumberTwoPowerQuery(number));

            logger.Log(LogLevel.Information, $"Proxy gateway - > sending response");

            if (!response)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
