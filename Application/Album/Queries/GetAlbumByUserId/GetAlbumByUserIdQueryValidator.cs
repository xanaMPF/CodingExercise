using FluentValidation;

namespace CodingExercise.Application.Album.Queries.GetSettingById
{
    public class GetAlbumByUserIdQueryValidator : AbstractValidator<GetAlbumByUserIdQuery>
    {
        private const string USER_NOT_VALID_MESSAGE = "UserId not valid";
        public GetAlbumByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(USER_NOT_VALID_MESSAGE);

        }
    }
}
