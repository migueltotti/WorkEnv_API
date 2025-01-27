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
        Guid workSpaceId,
        int maxNumberOfParticipants,
        Privacy privacy,
        ActivityStatus activityStatus,
        Access accessOptions,
        DateTime startDate,
        DateTime endDate)
        : base(workSpaceId, maxNumberOfParticipants, privacy, activityStatus, accessOptions)
    {
        if(startDate > endDate)
            throw new ArgumentException("Start date cannot be greater than end date");
            
        StartDate = startDate;
        EndDate = endDate;
    }

    public Task(
        Guid workSpaceId,
        int maxNumberOfParticipants,
        Privacy privacy,
        ActivityStatus activityStatus,
        Access accessOptions,
        DateTime startDate)
        : base(workSpaceId, maxNumberOfParticipants, privacy, activityStatus, accessOptions)
    {
        StartDate = startDate;
    }

    public void ChangeStartDate(DateTime newStartDate)
    {
        if (EndDate.Equals(newStartDate) || EndDate < newStartDate)
            throw new ArgumentException("The start date cannot be before the end date or equal end date.");
        
        StartDate = newStartDate;
    }

    public void ChangeEndDate(DateTime newEndDate)
    {
        if (StartDate.Equals(newEndDate) || StartDate > newEndDate)
            throw new ArgumentException("The end date cannot be before the start date or equal start date.");
        
        StartDate = newEndDate;
    }
}