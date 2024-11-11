using BarberBoss.Application.UseCases.Barbers.DoLogin;
using BarberBoss.Application.UseCases.Barbers.GetProfile;
using BarberBoss.Application.UseCases.Barbers.Update;
using BarberBoss.Application.UseCases.BarberShops.Update;
using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Domain.Enums;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers.Barbers;

[ApiController]
[Route("api/")]
public class BarbersController : ControllerBase
{
    [HttpPost]
    [Route("barbers/signin")]
    [ProducesResponseType(typeof(ResponseBarberDoLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ResponseBarberDoLoginJson>> DoLogin(
        [FromServices] IBarberDoLoginUseCase useCase, 
        [FromBody] RequestBarberDoLoginJson request)
    {
        var result = await useCase.Execute(request);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                nameof(ErrorCodes.InvalidCredential) => Unauthorized(new ResponseErrorJson(error.Message!)),   
                _ => BadRequest(),
            };
        }

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("barbers/profile")]
    [ProducesResponseType(typeof(ResponseBarberJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ResponseBarberJson>> GetProfile(
        [FromServices] IGetBarberProfileUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                nameof(ErrorCodes.NotFound) => NotFound(new ResponseErrorJson(error.Message!)),
                _ => BadRequest(),
            };
        }

        return Ok(result.Value);
    }

    [HttpPatch]
    [Authorize(Roles = nameof(UserType.Barber))]
    [Route("barbers/profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateProfile([FromServices] IUpdateBarberUseCase useCase, [FromBody] RequestBarberJson request)
    {
        var result = await useCase.Execute(request);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                nameof(ErrorCodes.NotFound) => NotFound(new ResponseErrorJson(error.Message!)),
                nameof(ErrorCodes.InternalServerError) => new ObjectResult(new ResponseErrorJson(error.Message!)),
                nameof(ErrorCodes.ErrorOnValidation) => BadRequest(new ResponseErrorJson(error.Messages!)),
                _ => BadRequest(),
            };
        }

        return NoContent();
    }
}
