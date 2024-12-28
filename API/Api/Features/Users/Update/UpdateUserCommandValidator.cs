﻿using System.Text.RegularExpressions;
using API.Shared.Constants;
using FluentValidation;

namespace API.Api.Features.Users.Update;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Request.FirstName).NotEmpty();
        RuleFor(x => x.Request.LastName).NotEmpty();
        RuleFor(x => x.Request.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Request.Phone).Must(x => x is null || Regex.IsMatch(x, GlobalConstants.PhoneRegex));
    }
}