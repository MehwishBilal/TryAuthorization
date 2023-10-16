using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

internal class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }
    private readonly AuthorizationOptions _options;

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
        // There can only be one policy provider in ASP.NET Core.
        // We only handle permissions related policies, for the rest
        /// we will use the default provider.
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        _options = options.Value;
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    // Dynamically creates a policy with a requirement that contains the permission.
    // The policy name must match the permission that is needed.
    public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        // Check static policies first
        var policy = await base.GetPolicyAsync(policyName);

        if (policy == null)
        {
            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();

            // Add policy to the AuthorizationOptions, so we don't have to re-create it each time
            _options.AddPolicy(policyName, policy);
        }

        return policy;

    }

    public async Task<AuthorizationPolicy> GetFallbackPolicyAsync()
    {
        return await FallbackPolicyProvider.GetDefaultPolicyAsync();
    }
}