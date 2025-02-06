using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private WorkEnvDbContext? _context;
    private IUserRepository? _userRepository;
    private IWorkSpaceRepository? _workSpaceRepository;
    private IEventRepository? _eventRepository;
    private ITaskRepository? _taskRepository;
    private IMessageRepository? _messageRepository;
    private IRoleRepository? _roleRepository;
    private IUserActivityRepository? _userActivityRepository;

    public UnitOfWork(WorkEnvDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository = _userRepository ?? new UserRepository(_context);
        }
        
    }

    public IWorkSpaceRepository WorkSpaceRepository
    {
        get
        {
            return _workSpaceRepository = _workSpaceRepository ?? new WorkSpaceRepository(_context);
        }
    }
    
    public IEventRepository EventRepository 
    {
        get
        {
            return _eventRepository = _eventRepository ?? new EventRepository(_context);
        }
    }
    
    public ITaskRepository TaskRepository 
    {
        get
        {
            return _taskRepository = _taskRepository ?? new TaskRepository(_context);
        }
    }
    
    public IMessageRepository MessageRepository 
    {
        get
        {
            return _messageRepository = _messageRepository ?? new MessageRepository(_context);
        }
    }
    
    public IRoleRepository RoleRepository 
    {
        get
        {
            return _roleRepository = _roleRepository ?? new RoleRepository(_context);
        }
    }
    
    
    public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Dispose()
    {
        await _context.DisposeAsync();
    }
}