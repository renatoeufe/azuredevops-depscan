using FluentValidation;

namespace depscan.api.Models
{
    public class ScanRequestValidator : AbstractValidator<ScanRequest>
    {
        public ScanRequestValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
            RuleFor(x => x.Feed).NotEmpty();
            RuleFor(x => x.Organization).NotEmpty();
            RuleFor(x => x.Project).NotEmpty();
            RuleFor(x => x.Repo).NotEmpty();
            RuleFor(x => x.User).NotEmpty();
        }
    }
}