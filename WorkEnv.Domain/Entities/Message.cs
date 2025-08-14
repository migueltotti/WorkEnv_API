using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public record Message
{
    public Guid MessageId { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime PublishedAt { get; private set; }

    // Message 0..* - 1 WorkSpace -> Composition
    public Guid WorkSpaceId { get; private set; }
    public WorkSpace? WorkSpace { get; private set; }
    
    // Message 0..* - 0..1 Activity -> Composition
    public Guid? ActivityId { get; private set; }
    public Activity? Activity { get; private set; }

    private Message()
    {
    }

    public Message(Guid messageId, string? title, string? content, DateTime publishedAt, Guid workSpaceId)
    {
        MessageId = messageId;
        Title = title;
        Content = content;
        PublishedAt = publishedAt;
        WorkSpaceId = workSpaceId;
    }

    public Message(string? title, string? content, DateTime publishedAt, Guid workSpaceId)
    {
        Title = title;
        Content = content;
        PublishedAt = publishedAt;
        WorkSpaceId = workSpaceId;
    }

    public Message(Guid messageId, string? title, string? content, DateTime publishedAt, Guid workSpaceId, Guid? activityId)
    {
        MessageId = messageId;
        Title = title;
        Content = content;
        PublishedAt = publishedAt;
        WorkSpaceId = workSpaceId;
        ActivityId = activityId;
    }

    public Message(string? title, string? content, DateTime publishedAt, Guid workSpaceId, Guid? activityId)
    {
        Title = title;
        Content = content;
        PublishedAt = publishedAt;
        WorkSpaceId = workSpaceId;
        ActivityId = activityId;
    }
}