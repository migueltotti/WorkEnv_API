using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Services;

public static class PrivacyService
{
    public static bool CheckPrivacy(Privacy privacy)
    {
        return privacy is Privacy.Private or Privacy.Public;
    }
}