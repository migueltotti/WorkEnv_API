using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public class Task : Activity
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private Task()
    {
    }

    public Task(
        Guid id,
        Guid workSpaceId,
        string name,
        int maxNumberOfParticipants,
        Privacy privacy,
        ActivityStatus activityStatus,
        Access accessOptions,
        DateTime startDate,
        DateTime endDate,
        Guid? adminId = null)
        : base(id, workSpaceId, name, maxNumberOfParticipants, privacy, activityStatus, accessOptions, adminId)
    {
        if(startDate > endDate)
            throw new ArgumentException("Start date cannot be greater than end date");
            
        StartDate = startDate;
        EndDate = endDate;
    }

    public void ChangeDate(Guid ownerOrAdminId, DateTime newStartDate, DateTime newEndDate)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");
        
        if (EndDate.Equals(newStartDate) || EndDate < newStartDate)
            throw new ArgumentException("The start date cannot be before the end date or equal end date.");
        
        if (StartDate.Equals(newEndDate) || StartDate > newEndDate)
            throw new ArgumentException("The end date cannot be before the start date or equal start date.");
        
        StartDate = newStartDate;
        EndDate = newEndDate;
    }
}