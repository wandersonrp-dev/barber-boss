using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.UnitOfWork;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.Register;
public class RegisterBarberShopUseCase : IRegisterBarberShopUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBarberShopRepository _barberShopRepository;
    private readonly ILogger<RegisterBarberShopUseCase> _logger;
    private readonly IPasswordHasher<BarberShop> _passwordHasher;

    public RegisterBarberShopUseCase(
        IUnitOfWork unitOfWork, 
        IBarberShopRepository barberShopRepository, 
        ILogger<RegisterBarberShopUseCase> logger, 
        IPasswordHasher<BarberShop> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _barberShopRepository = barberShopRepository;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<CustomResult<ResponseRegisterBarberShopJson>> Execute(RequestRegisterBarberShopJson request)
    {
        var result = Validate(request);

        if(result.Code == nameof(ErrorCodes.ErrorOnValidation))
        {
            _logger.LogError(message: nameof(ErrorCodes.ErrorOnValidation), args: result.Messages!.ToArray());
            return CustomResult<ResponseRegisterBarberShopJson>.Failure(result);
        }

        var barberShopExists = await _barberShopRepository.ExistsWithSameEmail(request.Email);

        if(barberShopExists)
        {
            _logger.LogError(message: $"{nameof(ErrorCodes.Conflict)}: {request.Email}", args: [ResourceErrorMessages.BARBER_SHOP_ALREADY_EXISTS]);
            return CustomResult<ResponseRegisterBarberShopJson>.Failure(CustomError.Conflict(ResourceErrorMessages.BARBER_SHOP_ALREADY_EXISTS));
        }        

        var barberShop = new BarberShop
        {
            Name = request.Name,
            Email = request.Email,            
            Phone = request.Phone,
            PhoneContact = request.PhoneContact,            
        };

        barberShop.Password = _passwordHasher.HashPassword(barberShop, request.Password);

        await _barberShopRepository.AddAsync(barberShop);

        await _unitOfWork.Commit();

        return CustomResult<ResponseRegisterBarberShopJson>.Success(new ResponseRegisterBarberShopJson(true));
    }

    private static CustomError Validate(RequestRegisterBarberShopJson request)
    {
        var validator = new RegisterBarberShopValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            return CustomError.ErrorOnValidation(errors);
        }

        return CustomError.None;
    }
}
