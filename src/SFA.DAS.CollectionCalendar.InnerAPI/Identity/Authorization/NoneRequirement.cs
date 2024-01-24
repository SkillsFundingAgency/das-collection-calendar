using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authorization;

[ExcludeFromCodeCoverage]
public class NoneRequirement : IAuthorizationRequirement { }