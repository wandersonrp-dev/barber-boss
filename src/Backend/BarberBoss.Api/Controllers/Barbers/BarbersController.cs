using BarberBoss.Application.UseCases.Barbers.DoLogin;
using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Exception.ExceptionsBase;
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
}
