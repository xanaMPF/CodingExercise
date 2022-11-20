using FluentValidation;

namespace CodingExercise.Application.Album.Queries.GetAlbumsByUserId
{
    public class GetAlbumByUserIdQueryValidator : AbstractValidator<GetAlbumsByUserIdQuery>
    {
        private const string USER_NOT_VALID_MESSAGE = "UserId not valid";
        public GetAlbumByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(USER_NOT_VALID_MESSAGE);

        }
    }
}
