using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authorization;

[ExcludeFromCodeCoverage]
public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder AllowAnonymousUser(this AuthorizationPolicyBuilder builder)
    {
        builder.Requirements.Add(new NoneRequirement());
        return builder;
    }
}