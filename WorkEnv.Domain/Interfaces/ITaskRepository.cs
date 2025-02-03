using WorkEnv.Domain.Entities;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Domain.Interfaces;

public interface ITaskRepository : IRepository<Task>
{
    
}