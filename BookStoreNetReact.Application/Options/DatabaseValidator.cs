﻿using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class DatabaseValidator : AbstractValidator<DatabaseOptions>
    {
        public DatabaseValidator()
        {
            RuleFor(c => c.ConnectionString).NotEmpty();
            RuleFor(c => c.CommandTimeout).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
