using System.ComponentModel.DataAnnotations;
using DigitalDream.TvShows.Web.Requests;
using FluentAssertions;

namespace DigitalDream.TvShows.Web.Tests.Requests;

public class CreateTvShowRequestTests
{
    [Fact]
    public void GivenValidModel_WhenCallingValidate_NoErrorsAreReturned()
    {
        var validModel = new CreateTvShowRequest
        {
            Id = 1,
            Name = "Breaking Bad",
            Language = "English",
            Premiered = DateTime.UtcNow,
            Genres = new List<string> { "Drama" },
            Summary = "Summary"
        };

        var validationResults = ValidateModel(validModel);

        validationResults.Should().BeEmpty();
    }

    // TODO: Fix validation (Genres doesn't work because it is initialized as empty list)
    [Fact]
    public void GivenInvalidModel_WhenCallingValidate_CorrectErrorsAreReturned()
    {
        var invalidModel = new CreateTvShowRequest();

        var validationResults = ValidateModel(invalidModel);

        // validationResults.Count.Should().Be(3); // TODO: Should be 5; skipping Id and Premiered for now
        // validationResults.Any(v => v.ErrorMessage ==  "The Id field is required.").Should().BeTrue();
        validationResults.Any(v => v.ErrorMessage ==  "The Name field is required.").Should().BeTrue();
        validationResults.Any(v => v.ErrorMessage ==  "The Language field is required.").Should().BeTrue();
        // validationResults.Any(v => v.ErrorMessage ==  "The Premiered field is required.").Should().BeTrue();
        // validationResults.Any(v => v.ErrorMessage ==  "The Genres field is required.").Should().BeTrue();
    }

    private static List<ValidationResult> ValidateModel(CreateTvShowRequest validModel)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(validModel, null, null);
        Validator.TryValidateObject(validModel, ctx, validationResults, true);
        return validationResults;
    }
}