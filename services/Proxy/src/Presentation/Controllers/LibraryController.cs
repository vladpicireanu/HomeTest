﻿using Application.Library.Queries;
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
    }
}