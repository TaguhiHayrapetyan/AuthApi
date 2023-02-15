using FluentValidation.Results;

namespace AuthService.Application.Exceptions;

public static class ThrowHelper
{
    public static void ThrowNotFoundException(string message)
    {
        throw new EntityNotFoundException(message);
    }
    public static void ThrowBadRequestException(string message)
    {
        throw new BadRequestException(message);
    }
    public static void ThrowBadRequestException(List<ValidationFailure> errors)
    {
        var errorMessage = "";
        errors.ForEach(e =>
        {
             errorMessage += $"{e.ErrorMessage}{Environment.NewLine}";
        });
        ThrowBadRequestException(errorMessage);
    }
}