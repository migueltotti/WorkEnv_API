namespace WorkEnv.Domain.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IWorkSpaceRepository WorkSpaceRepository { get; }
    public IEventRepository EventRepository { get; }
    public ITaskRepository TaskRepository { get; }
    public IMessageRepository MessageRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IUserActivityRepository UserActivityRepository { get; }
    void CommitChangesAsync(CancellationToken cancellationToken = default);
    void Dispose();
}