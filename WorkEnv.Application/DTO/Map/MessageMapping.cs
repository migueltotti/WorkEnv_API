using WorkEnv.Application.DTO.Message;

namespace WorkEnv.Application.DTO.Map;

public static class MessageMapping
{
    public static MessageDTO ToMessageDto(this Domain.Entities.Message message)
    {
        return new MessageDTO(
            message.MessageId,
            message.ActivityId,
            message.Title,
            message.Content,
            message.CreateDate,
            message.MessageType
            );
    }
}