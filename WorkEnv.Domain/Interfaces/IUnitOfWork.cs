namespace WorkEnv.Domain.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IWorkSpaceRepository WorkSpaceRepository { get; }
    public IEventRepository EventRepository { get; }
    public ITaskRepository TaskRepository { get; }
    public IMessageRepository MessageRepository { get; }
    public IRoleRepository RoleRepository { get; }
    Task CommitChangesAsync(CancellationToken cancellationToken = default);
    Task Dispose();
}