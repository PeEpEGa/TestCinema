using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestCinema.Contracts.Http;
using TestCinema.Domain.Commands;

namespace TestCinema.Api.Controllers;

[Route("api/cinema")]
public class CinemaController : BaseController
{
    private readonly IMediator _mediator;

    public CinemaController(IMediator mediator, ILogger<CinemaController> logger) : base(logger)
    {
        _mediator = mediator;
    }

    [HttpGet("getAllCinemas")]
    public Task<IActionResult> AddCinema(CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var query = new GetAllCinemasQuery
            {
            };

            var result = await _mediator.Send(query, cancellationToken);

            var response = new GetAllCinemasResponse
            {
                Cinemas = result.Cinemas
            };

            return Ok(response);
        }, cancellationToken);
    }


    [HttpGet("getCinemaHallById")]
    public Task<IActionResult> GetCinemaHallById(GetCinemaHallByIdRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var query = new GetCinemaHallByIdQuery
            {
                CinemaHallId = request.CinemaHallId
            };

            var result = await _mediator.Send(query, cancellationToken);

            var response = new GetCinemaHallByIdResponse
            {
                CinemaHall = result.CinemaHall
            };

            return Ok(response);
        }, cancellationToken);
    }

    [HttpPost("addCinema")]
    public Task<IActionResult> AddCinema([FromBody]CreateCinemaRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var command = new AddCinemaCommand
            {
                Name = request.Name,
                TotalHalls = request.TotalHalls,
                LocationId = request.LocationId
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new CreateCinemaResponse
            {
                CinemaId = result.Cinema.CinemaId
            };

            return Created($"http://{Request.Host}/api/cinema/{response.CinemaId}", response);
        }, cancellationToken);
    }


    [HttpPost("addCinemaHall")]
    public Task<IActionResult> AddCinemaHall([FromBody]CreateCinemaHallRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var command = new AddCinemaHallCommand
            {
                Name = request.Name,
                CinemaId = request.CinemaId,
                TotalSeats = request.TotalSeats
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new CreateCinemaHallResponse
            {
                CinemaHallId = result.CinemaHall.CinemaHallId
            };

            return Created($"http://{Request.Host}/api/cinema/{response.CinemaHallId}", response);
        }, cancellationToken);
    }


    [HttpPost("addCinemaHallSeat")]
    public Task<IActionResult> AddCinemaHallSeat([FromBody]CreateCinemaHallSeatRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var command = new AddCinemaHallSeatCommand
            {
                SeatRow = request.SeatRow,
                SeatColumn = request.SeatColumn,
                CinemaHallId = request.CinemaHallId,
                Price = request.Price
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new CreateCinemaHallSeatResponse
            {
                CinemaHallSeatId = result.CinemaHallSeat.CinemaHallSeatId
            };

            return Created($"http://{Request.Host}/api/cinema/{response.CinemaHallSeatId}", response);
        }, cancellationToken);
    }


    [HttpPut("updateCinema")]
    public Task<IActionResult> UpdateCinema([FromBody]UpdateCinemaRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var command = new UpdateCinemaCommand
            {
                Name = request.Name,
                TotalHalls = request.TotalHalls,
                Id = request.Id,
                LocationId = request.LocationId
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new UpdateCinemaResponse
            {
                CinemaId = result.Cinema.CinemaId
            };

            return Created($"http://{Request.Host}/api/cinema/{response.CinemaId}", response);
        }, cancellationToken);
    }


    [HttpDelete("deleteCinema")]
    public Task<IActionResult> DeleteCinema([FromBody]DeleteCinemaRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var command = new DeleteCinemaCommand
            {
                Id = request.Id
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new DeleteCinemaResponse
            {
                CinemaId = result.CinemaId
            };

            return NotFound();
        }, cancellationToken);
    }


    // [HttpDelete]
    // public Task<IActionResult> DeleteObjective(int objectiveId, CancellationToken cancellationToken)
    // {
    //     return SafeExecute(async () =>
    //     {
    //         var command = new DeleteObjectiveCommand
    //         {
    //             ObjectiveId = objectiveId
    //         };

    //         var result = await _mediator.Send(command, cancellationToken);

    //         var response = new ObjectiveResponse
    //         {
    //             Message = $"Objective with id: {objectiveId} was deleted."
    //         };

    //         return Ok(response);
    //     }, cancellationToken);
    // }


    // [HttpPut]
    // public Task<IActionResult> UpdateObjective(int objectiveId, string title, CancellationToken cancellationToken)
    // {
    //     return SafeExecute(async () =>
    //     {
    //         var command = new UpdateObjectiveCommand
    //         {
    //             ObjectiveId = objectiveId,
    //             Title = title
    //         };

    //         var result = await _mediator.Send(command, cancellationToken);

    //         var response = new ObjectiveResponse
    //         {
    //             Message = $"Objective with id: {objectiveId} was updated."
    //         };

    //         return Ok(response);
    //     }, cancellationToken);
    // }


    // [HttpGet]
    // [ProducesResponseType(typeof(ObjectivesResponse), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    // [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    // public Task<IActionResult> GetObjectives(CancellationToken cancellationToken)
    // {
    //     return SafeExecute(async () =>
    //     {
    //         var query = new GetAllObjectivesQuery{};

    //         var result = await _mediator.Send(query, cancellationToken);

    //         var response = new ObjectivesResponse
    //         {
    //             Objectives = result.Objectives
    //         };

    //         return Ok(response.Objectives);
    //     }, cancellationToken);
    // }
}