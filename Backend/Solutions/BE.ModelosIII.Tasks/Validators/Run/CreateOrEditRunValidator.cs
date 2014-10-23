using System;
using System.Net;
using System.Text.RegularExpressions;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Run;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Run
{
    public abstract class CreateOrEditRunValidator<T> : AbstractValidator<T>
        where T : RunCommand
    {
        protected CreateOrEditRunValidator()
        {
        }
    }
}
