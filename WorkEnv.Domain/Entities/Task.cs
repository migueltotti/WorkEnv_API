using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public class Task : Activity
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    public Task(int maxNumberOfParticipants, Guid id, Guid adminId, Guid workSpaceId, Privacy privacy, Access accessOptions, AdminInvite adminInviteCode, DateTime startDate, DateTime endDate) : base(maxNumberOfParticipants, id, adminId, workSpaceId, privacy, accessOptions, adminInviteCode)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public Task(int maxNumberOfParticipants, Guid adminId, Guid workSpaceId, Privacy privacy, Access accessOptions, DateTime startDate, DateTime endDate) : base(maxNumberOfParticipants, adminId, workSpaceId, privacy, accessOptions)
    {
        StartDate = startDate;
        EndDate = endDate;
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