using BE.ModelosIII.Tasks.Commands.Movie;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Movie
{
    public class CreateMovieValidator : CreateOrEditMovieValidator<CreateMovieCommand>
    {
        public CreateMovieValidator()
        {
            RuleFor(x => x.Id)
                .Equal(default(int));
        }
    }
}