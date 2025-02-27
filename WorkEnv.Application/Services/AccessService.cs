using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Services;

public static class AccessService
{
    public static bool CheckAccessOptions(Access access)
    {
        return access is Access.PasswordRequired or Access.OpenToAll;
    }
}