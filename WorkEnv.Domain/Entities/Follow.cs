using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public class Follow
{
    public Guid Id { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public DateTime? ResponseDate { get; private set; }
    public FollowStatus Status { get; private set; }
    
    // FollowRequest 0..* - 2 User -> Composition
    public Guid FollowerId { get; private set; }
    public User? Follower { get; private set; }
    
    public Guid FolloweeId { get; private set; }
    public User? Followee { get; private set; }

    private Follow()
    {
    }

    public Follow(Guid id, DateTime requestedAt, FollowStatus status, Guid followerId, Guid followeeId)
    {
        Id = id;
        RequestedAt = requestedAt;
        Status = status;
        FollowerId = followerId;
        FolloweeId = followeeId;
    }

    public Follow(DateTime requestedAt, FollowStatus status, Guid followerId, Guid followeeId)
    {
        RequestedAt = requestedAt;
        Status = status;
        FollowerId = followerId;
        FolloweeId = followeeId;
    }

    public void SendRequest(Guid followerId, Guid followeeId)
    {
        if(!FollowerId.Equals(followerId)) throw new ArgumentException("FollowerIds do not match");
        if(!FolloweeId.Equals(followeeId)) throw new ArgumentException("FolloweeIds do not match");

        ResponseDate = null;
        Status = FollowStatus.Pending;
    }

    public void AcceptRequest(Guid followeeId)
    {
        if(!FolloweeId.Equals(followeeId)) throw new ArgumentException("FolloweeIds do not match");
        
        ResponseDate = DateTime.UtcNow;
        Status = FollowStatus.Accepted;
    }
    
    public void RejectRequest(Guid followeeId)
    {
        if(!FolloweeId.Equals(followeeId)) throw new ArgumentException("FolloweeIds do not match");
        
        ResponseDate = DateTime.UtcNow;
        Status = FollowStatus.Rejected;
    }
    
    public void CancelRequest(Guid followerId)
    {
        if(!FollowerId.Equals(followerId)) throw new ArgumentException("FollowerIds do not match");
        
        ResponseDate = DateTime.UtcNow;
        Status = FollowStatus.Canceled;
    }
}