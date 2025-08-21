using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Services;

public static class AccessService
{
    public static bool CheckAccessOptions(EventAccessOption eventAccessOption)
    {
        return eventAccessOption is EventAccessOption.PasswordRequired or EventAccessOption.OpenToAll;
    }
}