using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IList<string> users = new List<string>
        { {"1"},{"2"}};

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {

        // If user does not have the scope claim, get out of here

        //if (!context.User.HasClaim(c => c.Type == "permissions"))
        //    return Task.CompletedTask;

        // Split the scopes string into an array

        //var permissions = context.User.FindAll(c => c.Type == "permissions").ToList();
        var permissions=users;

        // Succeed if the scope array contains the required scope
        // if (permissions.Any(s => s.Value == requirement.Permission))
        if (!permissions.Any(s => s.Contains(requirement.Permission)))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        else if (permissions.Any(s => s.Contains(requirement.Permission)))
        {
                    context.Succeed(requirement);
        }

        return Task.CompletedTask;
    
    }
}