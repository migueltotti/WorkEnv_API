using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Services;

public static class TypeService
{
    public static bool ChechType(MessageType type)
    {
        return type is MessageType.Comment or
                    MessageType.Warning or
                    MessageType.Important or
                    MessageType.ToDo;
    }
}