using BarberBoss.Application.UseCases.BarberShops.DoLogin;
using BarberBoss.Application.UseCases.BarberShops.GetProfile;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers.BarberShops;

[Route("api/")]
[ApiController]
public class BarberShopsController : ControllerBase
{
    [HttpPost]  
    [Route("barber-shops/signup")]
    [ProducesResponseType(typeof(ResponseRegisterBarberShopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResponseRegisterBarberShopJson>> SignUp(
        [FromServices] IRegisterBarberShopUseCase useCase, 
        [FromBody] RequestRegisterBarberShopJson request)
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

        return CreatedAtAction(nameof(SignUp), result.Value);
    }

    [HttpPost]
    [Route("barber-shops/signin")]
    [ProducesResponseType(typeof(ResponseBarberShopDoLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ResponseBarberShopDoLoginJson>> DoLogin(
        [FromServices] IBarberShopDoLoginUseCase useCase, 
        [FromBody] RequestBarberShopDoLoginJson request)
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
    [Authorize]
    [Route("barber-shops/perfil")]
    [ProducesResponseType(typeof(ResponseBarberShopJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseBarberShopJson>> GetProfile([FromServices] IGetBarberShopProfileUseCase useCase)
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
}
