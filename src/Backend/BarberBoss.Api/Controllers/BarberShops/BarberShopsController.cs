using BarberBoss.Application.UseCases.BarberShops;
using BarberBoss.Application.UseCases.BarberShops.CreateBarber;
using BarberBoss.Application.UseCases.BarberShops.CreateOpeningHour;
using BarberBoss.Application.UseCases.BarberShops.DoLogin;
using BarberBoss.Application.UseCases.BarberShops.GetAllBarbers;
using BarberBoss.Application.UseCases.BarberShops.GetOpeningHours;
using BarberBoss.Application.UseCases.BarberShops.GetProfile;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Application.UseCases.BarberShops.Update;
using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Communication.Responses.OpeningHour;
using BarberBoss.Domain.Enums;
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
    [Authorize (Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/profile")]
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

    [HttpPatch]
    [Authorize(Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateProfile([FromServices] IUpdateBarberShopUseCase useCase, [FromBody] RequestBarberShopJson request)
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

    [HttpPost]
    [Authorize(Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/barbers")]
    [ProducesResponseType(typeof(ResponseCreateBarberJson), StatusCodes.Status201Created)]    
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ResponseCreateBarberJson>> CreateBarber(
        [FromServices] ICreateBarberUseCase useCase, 
        [FromBody] RequestCreateBarberJson request)
    {
        var result = await useCase.Execute(request);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                nameof(ErrorCodes.Conflict) => Conflict(new ResponseErrorJson(error.Message!)),                
                nameof(ErrorCodes.ErrorOnValidation) => BadRequest(new ResponseErrorJson(error.Messages!)),
                _ => BadRequest(),
            };
        }

        return CreatedAtAction(nameof(CreateBarber), result.Value);
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/barbers")]
    [ProducesResponseType(typeof(ResponseGetAllBarbersJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseCreateBarberJson>> GetAllBarbers([FromServices] IGetAllBarbersUseCase useCase)
    {
        var result = await useCase.Execute();        

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/opening-hours")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> CreateOpeningHour([FromServices] ICreateOpeningHourUseCase useCase, [FromBody] RequestCreateOpeningHourJson request)
    {
        var result = await useCase.Execute(request);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                nameof(ErrorCodes.Conflict) => Conflict(new ResponseErrorJson(error.Message!)),
                nameof(ErrorCodes.NotFound) => NotFound(new ResponseErrorJson(error.Message!)),
                nameof(ErrorCodes.ErrorOnValidation) => BadRequest(new ResponseErrorJson(error.Messages!)),
                _ => BadRequest(),
            };
        }

        return CreatedAtAction(nameof(CreateOpeningHour), new
        {
            isCreated = result.Value,
        });
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/opening-hours")]
    [ProducesResponseType(typeof(ResponseGetOpeningHoursJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseGetOpeningHoursJson>> GetOpeningHours([FromServices] IGetOpeningHoursUseCase useCase, [FromQuery] DateTime dateFilter)
    {
        var result = await useCase.Execute(new RequestGetOpeningHoursJson { DateFilter = dateFilter });

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

    [HttpGet]
    [Authorize(Roles = nameof(UserType.BarberShop))]
    [Route("barber-shops/opening-hours/{id:Guid}")]
    [ProducesResponseType(typeof(ResponseOpeningHourJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseOpeningHourJson>> GetOpeningHourById([FromServices] IGetOpeningHourByIdUseCase useCase, [FromRoute] Guid id)
    {
        var result = await useCase.Execute(id);

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