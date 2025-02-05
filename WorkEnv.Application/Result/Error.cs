using System.Net;

namespace WorkEnv.Application.Result;

public record Error(
    string Code,
    string? Description = null,
    HttpStatusCode? HttpStatusCode = null
)
{
    public static readonly Error None = new(string.Empty);
}