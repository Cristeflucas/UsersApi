using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsersApi.Authorization
{
    public class AgeAuthorization : AuthorizationHandler<MinimumAge>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAge requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim == null)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);

            var ageUser = DateTime.Today.Year - dateOfBirth.Year;

            if (dateOfBirth > DateTime.Today.AddYears(-ageUser))
            {
                ageUser--;
            }

            if(ageUser >= requirement.Age)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
