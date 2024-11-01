using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers.BarberShops;

[Route("api/")]
[ApiController]
public class BarberShopsController : ControllerBase
{
    [HttpPost]
    [Route("barber-shops/signup")]
    [ProducesResponseType(typeof(ResponseRegisterBarberShopJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseRegisterBarberShopJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseRegisterBarberShopJson), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResponseRegisterBarberShopJson>> SignUp([FromServices] IRegisterBarberShopUseCase useCase, [FromBody] RequestRegisterBarberShopJson request)
    {
        var result = await useCase.Execute(request);

        if(result.IsFailure)
        {
            var error = result.Error;            

            return error.Code switch
            {
                nameof(ErrorCodes.ErrorOnValidation) => BadRequest(new ResponseErrorJson(error.Messages!)),
                nameof(ErrorCodes.Conflict) => Conflict(new ResponseErrorJson(error.Message!)),
                _ => BadRequest(),
            };
        }

        return Ok(result.Value);
    }
}
