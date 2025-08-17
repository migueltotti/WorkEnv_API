namespace WorkEnv.Domain.Enum;

public enum Permission : int
{
    None = 0,
    
    // ======== WorkSpace ========
    InviteWorkSpaceCollaborator = 11,
    RemoveWorkSpaceCollaborator = 12,
    CreateWorkSpaceRole = 13,
    EditWorkSpaceRole = 14,
    RemoveWorkSpaceRole = 15,
    AssignRoleToWorkSpaceCollaborator = 16,
    RemoveRoleFromWorkSpaceCollaborator = 17,
    
    // ======== Task ========
    CreateTask = 21,
    EditTask = 22,
    RemoveTask = 23,
    AssignResponsibleForTask = 24,
    RemoveResponsibleForTask = 25,
    
    // ======== Event ========
    CreateEvent = 31,
    EditEvent = 32,
    RemoveEvent = 33,
    InviteParticipantToEvent = 34,
    ChangeEventAdmin = 35,
    AssignRoleToEventParticipant = 36,
    RemoveRoleFromEventParticipant = 37,
    RemoveEventParticipant = 38,
    
    // ======== Message ========
    RegisterMessageToWorkSpace = 41,
    RegisterMessageToTask = 42,
    RegisterMessageToEvent = 43,
}