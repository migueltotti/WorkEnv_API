using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public record Message
{
    public Guid MessageId { get; private set; }
    public Guid ActivityId { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreateDate { get; private set; }
    public MessageType MessageType { get; private set; }
    
    public Activity Activity { get; private set; }

    private Message()
    {
    }

    public Message(Guid messageId, Guid activityId, string? title, string? content, MessageType messageType)
    {
        MessageId = messageId;
        ActivityId = activityId;
        Title = title;
        Content = content;
        CreateDate = DateTime.Now;
        MessageType = messageType;
    }
}