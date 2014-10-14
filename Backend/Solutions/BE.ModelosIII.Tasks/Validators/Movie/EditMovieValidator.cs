using BE.ModelosIII.Tasks.Commands.Movie;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Movie
{
    public class EditMovieValidator : CreateOrEditMovieValidator<EditMovieCommand>
    {
        public EditMovieValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(default(int));
        }
    }
}
