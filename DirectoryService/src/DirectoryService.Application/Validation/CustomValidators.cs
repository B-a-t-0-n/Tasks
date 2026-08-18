using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace DirectoryService.Application.Validation;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod.Invoke(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(JsonSerializer.Serialize(result.Error));
        });
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        return rule.WithMessage(JsonSerializer.Serialize(error));
    }
}

public static class ValidationExtensions
{
    public static Error ToError(this ValidationResult validationResult)
    {
        IEnumerable<ErrorMessage> messages = validationResult.Errors.SelectMany(ToErrorMessages);

        return Error.Validation(messages);
    }


    private static IReadOnlyList<ErrorMessage> ToErrorMessages(ValidationFailure failure)
    {
        if (TryParseError(failure.ErrorMessage, out Error? error))
        {
            return error.Messages;
        }

        return [new ErrorMessage("validation.failure", failure.ErrorMessage, failure.PropertyName)];
    }

    private static bool TryParseError(string message, [NotNullWhen(true)] out Error? error)
    {
        try
        {
            error = JsonSerializer.Deserialize<Error>(message);
            return error is not null;
        }
        catch (JsonException)
        {
            error = null;
            return false;
        }
    }
}
